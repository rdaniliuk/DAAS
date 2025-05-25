
using Moq;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API;
using Domain;

public class AccessRequestsControllerTests
{
    private readonly Mock<IAccessRequestService> _serviceMock;
    private readonly AccessRequestsController _controller;

    public AccessRequestsControllerTests()
    {
        _serviceMock = new Mock<IAccessRequestService>();
        _controller = new AccessRequestsController(_serviceMock.Object);
    }

    [Fact]
    public async Task CreateAccess_Valid_Returns()
    {
        var input = new CreateAccessRequestDto
        {
            DocumentId = "doc123",
            UserId = "user1",
            RequestedAccess = AccessType.Read,
            Reason = "Test reason"
        };

        var expected = new AccessRequestDto
        {
            Id = Guid.NewGuid(),
            DocumentId = input.DocumentId,
            UserId = input.UserId,
            RequestedAccess = input.RequestedAccess,
            Reason = input.Reason
        };

        _serviceMock.Setup(s => s.CreateAsync(input)).ReturnsAsync(expected);

        var result = await _controller.Create(input);

        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(_controller.GetById), createdAtActionResult.ActionName);
        Assert.Equal(expected, createdAtActionResult.Value);
    }

    [Fact]
    public async Task CreateAccess_Invalid_Returns()
    {
        _controller.ModelState.AddModelError("DocumentId", "Required");

        var result = await _controller.Create(new CreateAccessRequestDto());

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task GetById_Exists()
    {
        var id = Guid.NewGuid();
        var expected = new AccessRequestDto { Id = id };

        _serviceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(expected);

        var result = await _controller.GetById(id);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(expected, ok.Value);
    }

    [Fact]
    public async Task GetById_RequestNotFound()
    {
        var id = Guid.NewGuid();

        _serviceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync((AccessRequestDto)null);

        var result = await _controller.GetById(id);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Decide_Valid_Returns()
    {
        var id = Guid.NewGuid();
        var input = new DecisionInput
        {
            ApproverId = "user1",
            IsApproved = true,
            Comment = "approve"
        }; 
        
        var updated = new AccessRequestDto { Id = id };

        _serviceMock.Setup(s => s.DecideAsync(id, input.ApproverId, input.IsApproved, input.Comment))
            .ReturnsAsync(updated);

        var result = await _controller.Decide(id, input);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(updated, ok.Value);
    }
}
