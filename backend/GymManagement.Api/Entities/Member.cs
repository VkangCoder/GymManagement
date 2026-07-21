using GymManagement.Api.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GymManagement.Api.Entities;

[BsonIgnoreExtraElements]
public class Member
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } // null → driver tự generate ObjectId khi Insert

    [BsonElement("full_name")]
    public required string FullName { get; set; }

    [BsonElement("email")]
    public required string Email { get; set; }

    [BsonElement("phone_number")]
    public required string PhoneNumber { get; set; }

    [BsonElement("gender")]
    [BsonRepresentation(BsonType.String)]
    public Gender Gender { get; set; }

    [BsonElement("date_of_birth")]
    public DateTime DateOfBirth { get; set; }

    [BsonElement("join_date")]
    public DateTime JoinDate { get; set; }

    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public Status Status { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [BsonIgnore]
    public int Age
    {
        get
        {
            var today = DateTime.UtcNow.Date;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}