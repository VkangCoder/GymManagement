using GymManagement.Api.Entities;
using GymManagement.Api.Enums;
using GymManagement.Api.Exceptions;
using GymManagement.Api.Interfaces;
using GymManagement.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GymManagement.Api.Services;

public class MemberService : IMemberService
{
    private readonly IMongoCollection<Member> _members;

    public MemberService(IMongoClient client, IOptions<MongoDbSettings> settings)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _members = database.GetCollection<Member>("members");
    }

    public Task<List<Member>> GetAllAsync(CancellationToken ct = default) =>
        _members.Find(FilterDefinition<Member>.Empty).ToListAsync(ct);

    public async Task<Member?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        if (!ObjectId.TryParse(id, out _)) return null;
        return await _members.Find(m => m.Id == id).FirstOrDefaultAsync(ct);
    }

    public async Task<Member> CreateMemberAsync(Member member, CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        member.Email = member.Email.Trim().ToLowerInvariant(); // chuẩn hóa
        member.JoinDate = now;
        member.CreatedAt = now;
        member.UpdatedAt = null;
        member.Status = Status.Active;

        try
        {
            await _members.InsertOneAsync(member, cancellationToken: ct);
            return member;
        }
        catch (MongoWriteException ex) when (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
        {
            throw new DuplicateMemberException(ResolveDuplicateField(ex));
        }
    }

    private static string ResolveDuplicateField(MongoWriteException ex)
    {
        var msg = ex.WriteError.Message;
        if (msg.Contains("ux_members_email", StringComparison.OrdinalIgnoreCase)) return "Email";
        if (msg.Contains("ux_members_phone", StringComparison.OrdinalIgnoreCase)) return "PhoneNumber";
        return "Unknown";
    }

    public async Task<Member?> UpdateMemberAsync(string id, Member member, CancellationToken ct = default)
    {
        if (!ObjectId.TryParse(id, out _)) return null;

        // Partial update: chỉ đụng các field cho phép sửa.
        // KHÔNG set CreatedAt/JoinDate → giữ nguyên giá trị gốc.
        var update = Builders<Member>.Update
            .Set(m => m.FullName, member.FullName)
            .Set(m => m.Email, member.Email)
            .Set(m => m.PhoneNumber, member.PhoneNumber)
            .Set(m => m.Gender, member.Gender)
            .Set(m => m.DateOfBirth, member.DateOfBirth)
            .Set(m => m.Status, member.Status)
            .Set(m => m.UpdatedAt, DateTime.UtcNow);

        var options = new FindOneAndUpdateOptions<Member>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await _members.FindOneAndUpdateAsync(m => m.Id == id, update, options, ct);
    }

    public async Task<bool> DeleteMemberAsync(string id, CancellationToken ct = default)
    {
        if (!ObjectId.TryParse(id, out _)) return false;
        var result = await _members.DeleteOneAsync(m => m.Id == id, ct);
        return result.DeletedCount > 0;
    }
}