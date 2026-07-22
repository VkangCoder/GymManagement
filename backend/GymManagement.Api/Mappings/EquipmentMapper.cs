using GymManagement.Api.Entities;
using GymManagement.Api.Models;

namespace GymManagement.Api.Mappings;

public static class EquipmentMapper
{
    // Create DTO → Entity (Status/CreatedAt by Service )
    public static Equipment ToEntity(this CreateEquipmentRequest request) => new()
    {
        Name = request.Name,
        Category = request.Category!.Value,
        Brand = request.Brand,
        PurchaseDate = request.PurchaseDate,
        Price = request.Price
    };

    // Update DTO → Entity ( field allow to fix)
    public static Equipment ToEntity(this UpdateEquipmentRequest request) => new()
    {
        Name = request.Name,
        Category = request.Category!.Value,
        Brand = request.Brand,
        PurchaseDate = request.PurchaseDate,
        Status = request.Status!.Value,
        Price = request.Price
    };

    // Entity → Response DTO
    public static EquipmentResponse ToResponse(this Equipment entity) => new()
    {
        Id = entity.Id ?? string.Empty,
        Name = entity.Name,
        Category = entity.Category,
        Brand = entity.Brand,
        PurchaseDate = entity.PurchaseDate,
        Status = entity.Status,
        Price = entity.Price,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt
    };
}