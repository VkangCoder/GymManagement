using System.ComponentModel.DataAnnotations;
using GymManagement.Api.Enums;

namespace GymManagement.Api.Models;

public class CreateCoachRequest
{
    [Required(ErrorMessage = "Name must not be empty")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number must not be empty")]
    [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone must start with 0 and have exactly 10 digits.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [EnumDataType(typeof(Gender), ErrorMessage = "Invalid gender.")]
    public Gender Gender { get; set; }

    [Required(ErrorMessage = "Specialty must not be empty")]
    [EnumDataType(typeof(CoachSpecialty), ErrorMessage = "Invalid specialty.")]
    public CoachSpecialty Specialty { get; set; }

    [Range(0, 60, ErrorMessage = "Years of experience must be between 0 and 60.")]
    public int YearsOfExperience { get; set; }

    [Range(0, 1_000_000_000, ErrorMessage = "Session fee must be between 0 and 1,000,000,000.")]
    public decimal SessionFee { get; set; }
}
