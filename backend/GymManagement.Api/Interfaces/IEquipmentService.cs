using GymManagement.Api.Entities;

namespace GymManagement.Api.Interfaces;

public interface IEquipmentService
{
    Task<List<Equipment>> GetAllAsync(CancellationToken ct = default);
    Task<Equipment?> GetByIdAsync(string id, CancellationToken ct = default);
    Task<Equipment> CreateEquipmentAsync(Equipment equipment, CancellationToken ct = default);
    Task<Equipment?> UpdateEquipmentAsync(string id, Equipment equipment, CancellationToken ct = default);
    Task<bool> DeleteEquipmentAsync(string id, CancellationToken ct = default);
}