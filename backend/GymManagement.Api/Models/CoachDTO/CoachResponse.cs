using GymManagement.Api.Enums;

namespace GymManagement.Api.Models;

public class CoachResponse
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public CoachSpecialty Specialty { get; set; }
    public int YearsOfExperience { get; set; }
    public decimal SessionFee { get; set; }
    public CoachStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
