using GymManagement.Api.Entities;

namespace GymManagement.Api.Interfaces;

public interface IMemberService
{
    Task<List<Member>> GetAllAsync(CancellationToken ct = default);
    Task<Member?> GetByIdAsync(string id, CancellationToken ct = default);
    Task<Member> CreateMemberAsync(Member member, CancellationToken ct = default);
    Task<Member?> UpdateMemberAsync(string id, Member member, CancellationToken ct = default);
    Task<bool> DeleteMemberAsync(string id, CancellationToken ct = default);
}