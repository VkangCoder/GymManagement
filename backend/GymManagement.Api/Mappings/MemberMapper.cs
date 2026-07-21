using GymManagement.Api.Entities;
using GymManagement.Api.Models;

namespace GymManagement.Api.Mappings;

public static class MemberMapper
{
    public static Member ToEntity(this CreateMemberRequest request) => new()
    {
        FullName = request.FullName,
        Email = request.Email,
        PhoneNumber = request.PhoneNumber,
        Gender = request.Gender,
        DateOfBirth = request.DateOfBirth
        // JoinDate / Status / CreatedAt assigned by the Service
    };

    public static Member ToEntity(this UpdateMemberRequest request) => new()
    {
        FullName = request.FullName,
        Email = request.Email,
        PhoneNumber = request.PhoneNumber,
        Gender = request.Gender,
        DateOfBirth = request.DateOfBirth,
        Status = request.Status
    };

    public static MemberResponse ToResponse(this Member entity) => new()
    {
        Id = entity.Id ?? string.Empty,
        FullName = entity.FullName,
        Email = entity.Email,
        PhoneNumber = entity.PhoneNumber,
        Gender = entity.Gender,
        DateOfBirth = entity.DateOfBirth,
        Age = entity.Age,
        JoinDate = entity.JoinDate,
        Status = entity.Status,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt
    };
}