using GymManagement.Api.Entities;
using GymManagement.Api.Enums;
using GymManagement.Api.Interfaces;

namespace GymManagement.Api.Services;

public class EquipmentService : IEquipmentService
{
    private readonly IRepository<Equipment> _repository; 

    public EquipmentService(IRepository<Equipment> repository)  
    {
        _repository = repository;
    }

    public Task<List<Equipment>> GetAllAsync(CancellationToken ct = default) =>
        _repository.GetAllAsync(ct);

    public Task<Equipment?> GetByIdAsync(string id, CancellationToken ct = default) => _repository.GetByIdAsync(id, ct);

    public async Task<Equipment> CreateEquipmentAsync(Equipment e, CancellationToken ct = default)
    {
        e.CreatedAt = DateTime.UtcNow;
        e.Status = EquipmentStatus.Operational;

        await _repository.InsertAsync(e, ct);
        return e;
    }

    public async Task<Equipment?> UpdateEquipmentAsync(string id, Equipment e, CancellationToken ct = default)
    {
        var existing = await _repository.GetByIdAsync(id, ct);
        if (existing == null) return null;

        e.Id = id;
        e.CreatedAt = existing.CreatedAt;
        e.UpdatedAt = DateTime.UtcNow;

        var isSuccess = await _repository.ReplaceAsync(id, e, ct);
        return isSuccess ? e : null;
    }

    public Task<bool> DeleteEquipmentAsync(string id, CancellationToken ct = default) =>
         _repository.DeleteAsync(id, ct);
}