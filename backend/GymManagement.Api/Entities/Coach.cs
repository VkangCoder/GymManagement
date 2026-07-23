using GymManagement.Api.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GymManagement.Api.Entities;

[BsonIgnoreExtraElements]
public class Coach
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public Coach()
    {
        Id = ObjectId.GenerateNewId().ToString();
    }

    [BsonElement("full_name")]
    public required string FullName { get; set; }

    [BsonElement("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [BsonElement("specialty")]
    [BsonRepresentation(BsonType.String)]
    public CoachSpecialty Specialty { get; set; } = CoachSpecialty.Unknown;

    [BsonElement("years_of_experience")]
    public int YearsOfExperience { get; set; }

    [BsonElement("session_fee")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal SessionFee { get; set; }

    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public CoachStatus Status { get; set; } = CoachStatus.Active;

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}