using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccessRequestsController : ControllerBase
    {
        private readonly IAccessRequestService _service;
        public AccessRequestsController(IAccessRequestService service)
        {
            _service = service;
        }


        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create([FromBody] CreateAccessRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccessRequestDto>> GetById(Guid id)
        {
            var request = await _service.GetByIdAsync(id);
            if (request == null)
                return NotFound();
            return Ok(request);
        }

        [HttpGet("pending")]
        [Authorize(Roles = "Approver,Admin")]
        public async Task<ActionResult<IEnumerable<AccessRequestDto>>> GetPending()
        {
            var pendingRequests = await _service.GetPendingAsync();
            return Ok(pendingRequests);
        }

        [HttpPost("{id}/decision")]
        [Authorize(Roles = "Approver,Admin")]
        public async Task<ActionResult<AccessRequestDto>> Decide(Guid id, [FromBody] DecisionInput input)
        {
            var updatedRequest = await _service.DecideAsync(id, input.ApproverId, input.IsApproved, input.Comment);
            return Ok(updatedRequest);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<AccessRequestDto>>> GetAll()
        {
            var all = await _service.GetAllAsync();
            return Ok(all);
        }
    }
}
