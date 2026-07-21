using GymManagement.Api.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GymManagement.Api.Entities;

[BsonIgnoreExtraElements]
public class Equipment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public required string Name { get; set; }

    [BsonElement("category")]
    [BsonRepresentation(BsonType.String)]
    public EquipmentCategory Category { get; set; }

    [BsonElement("brand")]
    public string? Brand { get; set; }

    [BsonElement("purchase_date")]
    public DateTime PurchaseDate { get; set; }

    [BsonElement("status")]
    [BsonRepresentation(BsonType.String)]
    public EquipmentStatus Status { get; set; }

    [BsonElement("price")]
    [BsonRepresentation(BsonType.Decimal128)]
    public decimal Price { get; set; }

    [BsonElement("created_at")]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}