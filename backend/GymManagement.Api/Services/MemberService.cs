using GymManagement.Api.Entities;
using GymManagement.Api.Enums;
using GymManagement.Api.Exceptions;
using GymManagement.Api.Interfaces;
using MongoDB.Driver;

namespace GymManagement.Api.Services;

public class MemberService : IMemberService
{
    private readonly IRepository<Member> _repository;

    public MemberService(IRepository<Member> repository)
    {
        _repository = repository;
    }

    public Task<List<Member>> GetAllAsync(CancellationToken ct = default) =>
        _repository.GetAllAsync(ct);

    public Task<Member?> GetByIdAsync(string id, CancellationToken ct = default) => _repository.GetByIdAsync(id, ct);

    public async Task<Member> CreateMemberAsync(Member m, CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        m.Email = m.Email.Trim().ToLowerInvariant();
        m.JoinDate = now;
        m.CreatedAt = now;
        m.UpdatedAt = null;
        m.Status = Status.Active;

        try
        {
            await _repository.InsertAsync(m, ct);
            return m;
        }
        catch (DuplicateKeyException ex)
        {
            throw new DuplicateMemberException(ResolveDuplicateField(ex.ConstraintName!));
        }
    }

    public async Task<Member?> UpdateMemberAsync(string id, Member m, CancellationToken ct = default)
    {
        var existing = await _repository.GetByIdAsync(id, ct);
        if (existing == null) return null;

        m.Id = id;
        m.CreatedAt = existing.CreatedAt;
        m.UpdatedAt = DateTime.UtcNow;

        var isSuccess = await _repository.ReplaceAsync(id, m, ct);
        return isSuccess ? m : null;
    }

    public Task<bool> DeleteMemberAsync(string id, CancellationToken ct = default) => _repository.DeleteAsync(id, ct);

    private static string ResolveDuplicateField(string errorMessage)
    {
        if (errorMessage.Contains("ux_members_email", StringComparison.OrdinalIgnoreCase)) return "Email";
        if (errorMessage.Contains("ux_members_phone", StringComparison.OrdinalIgnoreCase)) return "PhoneNumber";
        return "Unknown";
    }
}