using UniversityHelper.TimetableService.Models.Db;
using UniversityHelper.TimetableService.Models.Dto;

namespace UniversityHelper.TimetableService.Mappers;

public static class SubjectMapper
{
    public static SubjectDto ToDto(this DbSubject subject)
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
            Date = subject.Date,
            Professor = subject.Professor,
            PointId = subject.PointId,
            Place = subject.Place
        };
    }
} 