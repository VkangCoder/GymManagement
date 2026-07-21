using System.ComponentModel.DataAnnotations;
using GymManagement.Api.Enums;

namespace GymManagement.Api.Models;

public class CreateEquipmentRequest
{
    [Required(ErrorMessage = "Equipment name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Equipment name must be between 2 and 100 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Equipment category is required.")]
    [EnumDataType(typeof(EquipmentCategory), ErrorMessage = "Invalid equipment category.")]
    public EquipmentCategory? Category { get; set; }

    [StringLength(50, ErrorMessage = "Brand name cannot exceed 50 characters.")]
    public string? Brand { get; set; }

    [Required(ErrorMessage = "Purchase date is required.")]
    public DateTime PurchaseDate { get; set; }

    [Required(ErrorMessage = "Price is required.")]
    [Range(0, 1_000_000_000, ErrorMessage = "Price must be between 0 and 1,000,000,000.")]
    public decimal Price { get; set; }
}