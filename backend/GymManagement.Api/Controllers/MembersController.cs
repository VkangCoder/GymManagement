using GymManagement.Api.Exceptions;
using GymManagement.Api.Interfaces;
using GymManagement.Api.Mappings;
using GymManagement.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers;

[ApiController]
[Route("api/members")]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService) => _memberService = memberService;

    [HttpGet]
    public async Task<ActionResult<List<MemberResponse>>> GetAll(CancellationToken ct)
    {
        var members = await _memberService.GetAllAsync(ct);
        return Ok(members.Select(m => m.ToResponse()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberResponse>> GetById(string id, CancellationToken ct)
    {
        var member = await _memberService.GetByIdAsync(id, ct);
        return member is null ? NotFound() : Ok(member.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<MemberResponse>> CreateMember(
        [FromBody] CreateMemberRequest request, CancellationToken ct)
    {
        try
        {
            var created = await _memberService.CreateMemberAsync(request.ToEntity(), ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToResponse());
        }
        catch (DuplicateMemberException ex)
        {
            return Conflict(new { field = ex.Field, message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MemberResponse>> UpdateMember(
        string id, [FromBody] UpdateMemberRequest request, CancellationToken ct)
    {
        try
        {
            var updated = await _memberService.UpdateMemberAsync(id, request.ToEntity(), ct);
            return updated is null
                ? NotFound($"Không tìm thấy hội viên có Id: {id} để cập nhật.")
                : Ok(updated.ToResponse());
        }
        catch (DuplicateMemberException ex)
        {
            return Conflict(new { field = ex.Field, message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMember(string id, CancellationToken ct)
    {
        var isDeleted = await _memberService.DeleteMemberAsync(id, ct);
        return isDeleted
            ? NoContent()
            : NotFound($"Không tìm thấy hội viên có Id: {id} để xóa.");
    }
}