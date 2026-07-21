using System.ComponentModel.DataAnnotations;
using GymManagement.Api.Enums;

namespace GymManagement.Api.Models;

public class CreateMemberRequest
{
    [Required(ErrorMessage = "Name must not be empty")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email must not be empty")]
    [EmailAddress(ErrorMessage = "Invalid Email.")]
    [StringLength(100, ErrorMessage = "Email must not exceed 100 characters.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number must not be empty")]
    [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone must start with 0 and have exactly 10 digits.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [EnumDataType(typeof(Gender), ErrorMessage = "Invalid gender.")]
    public Gender Gender { get; set; }

    [Required(ErrorMessage = "Date of birth must not be empty")]
    public DateTime DateOfBirth { get; set; }
}