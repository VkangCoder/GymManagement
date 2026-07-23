using GymManagement.Api.Interfaces;
using GymManagement.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GymManagement.Api.Repositories;


public class MongoRepository<T> : IRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    
    public MongoRepository(
        IMongoClient client,
        IOptions<MongoDbSettings> settings,
        string collectionName)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<T>(collectionName);
    }

   
    public async Task<List<T>> GetAllAsync(CancellationToken ct = default) =>
        await _collection.Find(_ => true).ToListAsync(ct);

    public async Task<T?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        if (!ObjectId.TryParse(id, out _)) return null;
        var filter = Builders<T>.Filter.Eq("Id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync(ct);

    }

    public async Task InsertAsync(T entity, CancellationToken ct = default) =>
        await _collection.InsertOneAsync(entity, cancellationToken: ct);

    public async Task<bool> ReplaceAsync(string id, T entity, CancellationToken ct = default)
    {
        if (!ObjectId.TryParse(id, out _)) return false;
        var filter = Builders<T>.Filter.Eq("Id", id);
        var result = await _collection.ReplaceOneAsync(filter, entity, cancellationToken: ct);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
    {
        if (!ObjectId.TryParse(id, out _)) return false;
        var filter = Builders<T>.Filter.Eq("Id", id);
        var result = await _collection.DeleteOneAsync(filter, ct);
        return result.DeletedCount > 0;
    }
}