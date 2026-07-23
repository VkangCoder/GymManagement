using GymManagement.Api.Entities;
using GymManagement.Api.Enums;
using GymManagement.Api.Exceptions;
using GymManagement.Api.Interfaces;

namespace GymManagement.Api.Services;

public class CoachService : ICoachService
{
    private readonly IRepository<Coach> _repository;

    public CoachService(IRepository<Coach> repository)
    {
        _repository = repository;
    }

    public Task<List<Coach>> GetAllAsync(CancellationToken ct = default) =>
        _repository.GetAllAsync(ct);

    public Task<Coach?> GetByIdAsync(string id, CancellationToken ct = default) => _repository.GetByIdAsync(id, ct);

    public async Task<Coach> CreateCoachAsync(Coach coach, CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        coach.CreatedAt = now;
        coach.UpdatedAt = null;
        coach.Status = CoachStatus.Active;

        try
        {
            await _repository.InsertAsync(coach, ct);
            return coach;
        }
        catch (DuplicateKeyException ex)
        {
            throw new DuplicateMemberException(ResolveDuplicateField(ex.ConstraintName!));
        }

        // await _repository.InsertAsync(coach, ct);
        // return coach;
    }

    public async Task<Coach?> UpdateCoachAsync(string id, Coach coach, CancellationToken ct = default)
    {
        var existing = await _repository.GetByIdAsync(id, ct);
        if (existing == null) return null;

        coach.Id = id;
        coach.CreatedAt = existing.CreatedAt;
        coach.UpdatedAt = DateTime.UtcNow;

        var isSuccess = await _repository.ReplaceAsync(id, coach, ct);
        return isSuccess ? coach : null;
    }

    public Task<bool> DeleteCoachAsync(string id, CancellationToken ct = default) => _repository.DeleteAsync(id, ct);

    private static string ResolveDuplicateField(string errorMessage)
    {
        if (errorMessage.Contains("ux_coaches_phone", StringComparison.OrdinalIgnoreCase)) return "PhoneNumber";
        return "Unknown";
    }
}
