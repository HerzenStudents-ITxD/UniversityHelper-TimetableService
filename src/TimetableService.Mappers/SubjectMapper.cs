using UniversityHelper.TimetableService.Data.Models;
using UniversityHelper.TimetableService.Models.Dto;

namespace UniversityHelper.TimetableService.Mappers;

public static class SubjectMapper
{
    public static SubjectDto ToDto(this Subject subject)
    {
        if (subject == null)
        {
            return null;
        }

        return new SubjectDto
        {
            Id = subject.Id,
            GroupId = subject.GroupId,
            Name = subject.Name,
            FullName = subject.FullName,
            Teacher = subject.Teacher,
            Room = subject.Room,
            Building = subject.Building,
            StartTime = subject.StartTime,
            EndTime = subject.EndTime,
            Type = subject.Type,
            Subgroup = subject.Subgroup,
            Comment = subject.Comment,
            IsOnline = subject.IsOnline,
            OnlineLink = subject.OnlineLink,
            Color = subject.Color,
            CreatedAt = subject.CreatedAt,
            UpdatedAt = subject.UpdatedAt
        };
    }
} 