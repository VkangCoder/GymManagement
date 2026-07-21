using GymManagement.Api.Entities;
using GymManagement.Api.Exceptions;
using GymManagement.Api.Interfaces;
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
    public async Task<ActionResult<List<Member>>> GetAll(CancellationToken ct)
    {
        var members = await _memberService.GetAllAsync(ct);
        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Member>> GetById(string id, CancellationToken ct)
    {
        var member = await _memberService.GetByIdAsync(id, ct);
        return member is null ? NotFound() : Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult<Member>> CreateMember(
    [FromBody] CreateMemberRequest request, CancellationToken ct)
    {
        var newMember = new Member
        {
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth
        };

        try
        {
            var created = await _memberService.CreateMemberAsync(newMember, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (DuplicateMemberException ex)
        {
            return Conflict(new { field = ex.Field, message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Member>> UpdateMember(
        string id, [FromBody] UpdateMemberRequest request, CancellationToken ct)
    {
        var memberToUpdate = new Member
        {
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth,
            Status = request.Status
        };

        var updated = await _memberService.UpdateMemberAsync(id, memberToUpdate, ct);
        return updated is null
            ? NotFound($"Không tìm thấy hội viên có Id: {id} để cập nhật.")
            : Ok(updated);
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