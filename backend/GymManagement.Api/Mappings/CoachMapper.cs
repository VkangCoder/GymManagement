using GymManagement.Api.Entities;
using GymManagement.Api.Models;

namespace GymManagement.Api.Mappings;

public static class CoachMapper
{
    public static Coach ToEntity(this CreateCoachRequest request) => new()
    {
        FullName = request.FullName,
        PhoneNumber = request.PhoneNumber,
        Gender = request.Gender,
        Specialty = request.Specialty,
        YearsOfExperience = request.YearsOfExperience,
        SessionFee = request.SessionFee
        // Status / CreatedAt / UpdatedAt assigned by the Service
    };

    public static Coach ToEntity(this UpdateCoachRequest request) => new()
    {
        FullName = request.FullName,
        PhoneNumber = request.PhoneNumber,
        Gender = request.Gender,
        Specialty = request.Specialty,
        YearsOfExperience = request.YearsOfExperience,
        SessionFee = request.SessionFee,
        Status = request.Status
    };

    public static CoachResponse ToResponse(this Coach entity) => new()
    {
        Id = entity.Id ?? string.Empty,
        FullName = entity.FullName,
        PhoneNumber = entity.PhoneNumber,
        Gender = entity.Gender,
        Specialty = entity.Specialty,
        YearsOfExperience = entity.YearsOfExperience,
        SessionFee = entity.SessionFee,
        Status = entity.Status,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt
    };
}
