using GymManagement.Api.Entities;
using GymManagement.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GymManagement.Api.Infrastructure;

public class MongoIndexInitializer : IHostedService
{
    private readonly IMongoClient _client;
    private readonly MongoDbSettings _settings;
    private readonly ILogger<MongoIndexInitializer> _logger;

    public MongoIndexInitializer(
        IMongoClient client,
        IOptions<MongoDbSettings> settings,
        ILogger<MongoIndexInitializer> logger)
    {
        _client = client;
        _settings = settings.Value;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken ct)
    {
        //Member
        var members = _client.GetDatabase(_settings.DatabaseName).GetCollection<Member>("members");

        var emailIndex = new CreateIndexModel<Member>(
            Builders<Member>.IndexKeys.Ascending(m => m.Email),
            new CreateIndexOptions { Unique = true, Name = "ux_members_email" });

        var phoneIndex = new CreateIndexModel<Member>(
            Builders<Member>.IndexKeys.Ascending(m => m.PhoneNumber),
            new CreateIndexOptions { Unique = true, Name = "ux_members_phone" });

        await members.Indexes.CreateManyAsync(new[] { emailIndex, phoneIndex }, ct);
        _logger.LogInformation("MongoDB indexes ensured for 'members'.");


        //Coach
        var coaches = _client.GetDatabase(_settings.DatabaseName).GetCollection<Coach>("coaches");

        var coachPhoneIndex = new CreateIndexModel<Coach>(
            Builders<Coach>.IndexKeys.Ascending(c => c.PhoneNumber),
            new CreateIndexOptions { Unique = true, Name = "ux_coaches_phone" });

        await coaches.Indexes.CreateManyAsync(new[] { coachPhoneIndex }, ct);
        _logger.LogInformation("MongoDB indexes ensured for 'coaches'.");

        //Coach
        var equipments = _client.GetDatabase(_settings.DatabaseName).GetCollection<Equipment>("equipments");

        var equipmentsBrand = new CreateIndexModel<Equipment>(
            Builders<Equipment>.IndexKeys.Ascending(c => c.Brand),
            new CreateIndexOptions { Unique = true, Name = "ux_equipments_brand" });

        await equipments.Indexes.CreateManyAsync(new[] { equipmentsBrand }, ct);
        _logger.LogInformation("MongoDB indexes ensured for 'equipments'.");
    }

    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}