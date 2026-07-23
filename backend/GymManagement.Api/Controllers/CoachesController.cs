using GymManagement.Api.Exceptions;
using GymManagement.Api.Interfaces;
using GymManagement.Api.Mappings;
using GymManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("api/coaches")]
public class CoachesController : ControllerBase
{
    private readonly ICoachService _coachService;

    public CoachesController(ICoachService coachService) => _coachService = coachService;

    [HttpGet]
    public async Task<ActionResult<List<CoachResponse>>> GetAll(CancellationToken ct)
    {
        var members = await _coachService.GetAllAsync(ct);
        return Ok(members.Select(m => m.ToResponse()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CoachResponse>> GetById(string id, CancellationToken ct)
    {
        var member = await _coachService.GetByIdAsync(id, ct);
        return member is null ? NotFound() : Ok(member.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<CoachResponse>> CreateMember(
        [FromBody] CreateCoachRequest request, CancellationToken ct)
    {
        try
        {
            var created = await _coachService.CreateCoachAsync(request.ToEntity(), ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
        }
        catch (DuplicateMemberException ex)
        {
            return Conflict(new { field = ex.Field, message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MemberResponse>> UpdateMember(
        string id, [FromBody] UpdateCoachRequest request, CancellationToken ct)
    {
        try
        {
            var updated = await _coachService.UpdateCoachAsync(id, request.ToEntity(), ct);
            return updated is null
                ? NotFound($"Cannot found coach with Id: {id} to update.")
                : Ok(updated.ToResponse());
        }
        catch (DuplicateMemberException ex)
        {
            return Conflict(new { field = ex.Field, message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCoachAsync(string id, CancellationToken ct)
    {
        var isDeleted = await _coachService.DeleteCoachAsync(id, ct);
        return isDeleted
            ? NoContent()
            : NotFound($"Cannot found coach with Id: {id} to delete");
    }
}