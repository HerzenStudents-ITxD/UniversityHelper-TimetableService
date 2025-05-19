using UniversityHelper.TimetableService.Data.Models;
using UniversityHelper.TimetableService.Models.Dto;

namespace UniversityHelper.TimetableService.Mappers;

public static class TimetableChangeMapper
{
    public static TimetableChangeDto ToDto(this TimetableChange change)
    {
        if (change == null) return null;
        return new TimetableChangeDto
        {
            Id = change.Id,
            SubjectId = change.SubjectId,
            GroupId = change.GroupId,
            OriginalDate = change.OriginalDate,
            NewDate = change.NewDate,
            OriginalRoom = change.OriginalRoom,
            NewRoom = change.NewRoom,
            OriginalTeacher = change.OriginalTeacher,
            NewTeacher = change.NewTeacher,
            Reason = change.Reason,
            CreatedAt = change.CreatedAt,
            UpdatedAt = change.UpdatedAt
        };
    }
} 