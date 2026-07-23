using GymManagement.Api.Entities;

namespace GymManagement.Api.Interfaces;

public interface ICoachService
{
    Task<List<Coach>> GetAllAsync(CancellationToken ct = default);
    Task<Coach?> GetByIdAsync(string id, CancellationToken ct = default);
    Task<Coach> CreateCoachAsync(Coach coach, CancellationToken ct = default);
    Task<Coach?> UpdateCoachAsync(string id, Coach coach, CancellationToken ct = default);
    Task<bool> DeleteCoachAsync(string id, CancellationToken ct = default);
}