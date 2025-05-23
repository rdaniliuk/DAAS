using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class AccessRequestService : IAccessRequestService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public AccessRequestService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<AccessRequestDto> CreateAsync(CreateAccessRequestDto dto)
        {
            var request = new AccessRequest
            {
                Id = Guid.NewGuid(),
                DocumentId = dto.DocumentId,
                UserId = dto.UserId,
                RequestedAccess = dto.RequestedAccess,
                Reason = dto.Reason
            };

            _db.AccessRequests.Add(request);
            await _db.SaveChangesAsync();

            return _mapper.Map<AccessRequestDto>(request);
        }

        public async Task<IEnumerable<AccessRequestDto>> GetPendingAsync()
        {
            var pendingRequests = await _db.AccessRequests
                .Where(r => r.Status == RequestStatus.Pending)
                .ToListAsync();

            return _mapper.Map<List<AccessRequestDto>>(pendingRequests);
        }

        public async Task<AccessRequestDto> DecideAsync(Guid requestId, string approverId, bool approve, string comment)
        {
            var request = await _db.AccessRequests
                .Include(r => r.Decision)
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null)
                throw new KeyNotFoundException($"Request {requestId} did't found");

            var decision = new Decision
            {
                Id = Guid.NewGuid(),
                AccessRequestId = requestId,
                ApproverId = approverId,
                IsApproved = approve,
                Comment = comment
            };

            _db.Decisions.Add(decision);

            request.Status = approve ? RequestStatus.Approved : RequestStatus.Rejected;
            request.Decision = decision;

            await _db.SaveChangesAsync();

            return _mapper.Map<AccessRequestDto>(request);
        }

        public async Task<AccessRequestDto> GetByIdAsync(Guid requestId)
        {
            var request = await _db.AccessRequests
                .Include(r => r.Decision)
                .FirstOrDefaultAsync(r => r.Id == requestId);

            if (request == null)
                throw new KeyNotFoundException($"Request {requestId} did't found");

            return _mapper.Map<AccessRequestDto>(request);
        }
    }
}
