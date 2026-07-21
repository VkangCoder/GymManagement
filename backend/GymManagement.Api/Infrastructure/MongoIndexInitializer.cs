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
        var database = _client.GetDatabase(_settings.DatabaseName);
        var members = database.GetCollection<Member>("members");

        // Email: unique + case-insensitive (collation strength 2)
        var emailIndex = new CreateIndexModel<Member>(
            Builders<Member>.IndexKeys.Ascending(m => m.Email),
            new CreateIndexOptions
            {
                Unique = true,
                Name = "ux_members_email",
                Collation = new Collation("en", strength: CollationStrength.Secondary)
            });

        // Phone: unique
        var phoneIndex = new CreateIndexModel<Member>(
            Builders<Member>.IndexKeys.Ascending(m => m.PhoneNumber),
            new CreateIndexOptions
            {
                Unique = true,
                Name = "ux_members_phone"
            });

        await members.Indexes.CreateManyAsync(new[] { emailIndex, phoneIndex }, ct);
        _logger.LogInformation("MongoDB indexes ensured for 'members' collection.");
    }

    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}