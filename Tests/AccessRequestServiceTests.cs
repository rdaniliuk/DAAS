using Microsoft.EntityFrameworkCore;
using Application.Services;
using Application.DTOs;
using AutoMapper;
using Infrastructure;
using Domain;

public class AccessRequestServiceTests
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly AccessRequestService _service;

    public AccessRequestServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new AppDbContext(options);

        var config = new MapperConfiguration(cfg => cfg.AddProfile<Application.Mapping.MappingProfile>());
        _mapper = config.CreateMapper();

        _service = new AccessRequestService(_dbContext, _mapper);
    }

    [Fact]
    public async Task CreatesRequest()
    {
        var dto = new CreateAccessRequestDto
        {
            DocumentId = "doc123",
            UserId = "user1",
            RequestedAccess = AccessType.Read,
            Reason = "test reason"
        };

        var result = await _service.CreateAsync(dto);

        Assert.NotNull(result);
        Assert.Equal("doc123", result.DocumentId);
        Assert.Equal("user1", result.UserId);

        var entity = await _dbContext.AccessRequests.FindAsync(result.Id);
        Assert.NotNull(entity);
    }

    [Fact]
    public async Task GetById_WhenExists()
    {
        var request = new AccessRequest
        {
            Id = Guid.NewGuid(),
            DocumentId = "doc123",
            UserId = "user1",
            RequestedAccess = AccessType.Edit,
            Reason = "test reason",
            Status = RequestStatus.Pending
        };

        _dbContext.AccessRequests.Add(request);
        await _dbContext.SaveChangesAsync();

        var result = await _service.GetByIdAsync(request.Id);

        Assert.NotNull(result);
        Assert.Equal(request.Id, result.Id);
    }

    [Fact]
    public async Task GetById_WhenNotFound()
    {
        var id = Guid.NewGuid();

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(id));
    }

    [Fact]
    public async Task Decide_Approves()
    {
        var request = new AccessRequest
        {
            Id = Guid.NewGuid(),
            DocumentId = "doc123",
            UserId = "uesr1",
            RequestedAccess = AccessType.Read,
            Reason = "test reason",
            Status = RequestStatus.Pending
        };

        _dbContext.AccessRequests.Add(request);
        await _dbContext.SaveChangesAsync();

        var result = await _service.DecideAsync(request.Id, "approver1", true, "approved");

        Assert.NotNull(result);
        Assert.Equal(RequestStatus.Approved, result.Status);

        var decision = _dbContext.Decisions.SingleOrDefault(d => d.AccessRequestId == request.Id);
        Assert.NotNull(decision);
        Assert.Equal("approver1", decision.ApproverId);
        Assert.True(decision.IsApproved);
        Assert.Equal("approved", decision.Comment);
    }
}
