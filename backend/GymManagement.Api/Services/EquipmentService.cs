using GymManagement.Api.Entities;
using GymManagement.Api.Enums;
using GymManagement.Api.Interfaces;
using GymManagement.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GymManagement.Api.Services;

public class EquipmentService : IEquipmentService
{
    private readonly IMongoCollection<Equipment> _equipments;

    public EquipmentService(IMongoClient client, IOptions<MongoDbSettings> settings)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _equipments = database.GetCollection<Equipment>("equipments");
    }

    public Task<List<Equipment>> GetAllAsync(CancellationToken ct = default) =>
        _equipments.Find(FilterDefinition<Equipment>.Empty).ToListAsync(ct);

    public async Task<Equipment?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        if (!ObjectId.TryParse(id, out _)) return null;
        return await _equipments.Find(e => e.Id == id).FirstOrDefaultAsync(ct);
    }

    public async Task<Equipment> CreateEquipmentAsync(Equipment equipment, CancellationToken ct = default)
    {
        equipment.CreatedAt = DateTime.UtcNow;
        equipment.UpdatedAt = null;
        equipment.Status = EquipmentStatus.Operational;

        await _equipments.InsertOneAsync(equipment, cancellationToken: ct);
        return equipment;
    }

    public async Task<Equipment?> UpdateEquipmentAsync(string id, Equipment equipment, CancellationToken ct = default)
    {
        if (!ObjectId.TryParse(id, out _)) return null;


        var update = Builders<Equipment>.Update
            .Set(e => e.Name, equipment.Name)
            .Set(e => e.Category, equipment.Category)
            .Set(e => e.Brand, equipment.Brand)
            .Set(e => e.PurchaseDate, equipment.PurchaseDate)
            .Set(e => e.Status, equipment.Status)
            .Set(e => e.Price, equipment.Price)
            .Set(e => e.UpdatedAt, DateTime.UtcNow);

        var options = new FindOneAndUpdateOptions<Equipment>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await _equipments.FindOneAndUpdateAsync(e => e.Id == id, update, options, ct);
    }

    public async Task<bool> DeleteEquipmentAsync(string id, CancellationToken ct = default)
    {
        if (!ObjectId.TryParse(id, out _)) return false;
        var result = await _equipments.DeleteOneAsync(e => e.Id == id, ct);
        return result.DeletedCount > 0;
    }
}