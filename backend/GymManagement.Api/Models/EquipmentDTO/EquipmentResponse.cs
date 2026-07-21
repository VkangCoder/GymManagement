using GymManagement.Api.Enums;

namespace GymManagement.Api.Models;

public class EquipmentResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public EquipmentCategory Category { get; set; }
    public string? Brand { get; set; }
    public DateTime PurchaseDate { get; set; }
    public EquipmentStatus Status { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}