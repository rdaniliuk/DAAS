using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAccessRequestService
    {
        Task<AccessRequestDto> CreateAsync(CreateAccessRequestDto dto);
        Task<IEnumerable<AccessRequestDto>> GetPendingAsync();
        Task<AccessRequestDto> DecideAsync(Guid requestId, string approverId, bool approve, string comment);
        Task<AccessRequestDto> GetByIdAsync(Guid requestId);
    }
}
