using System.Linq;
using UniversityHelper.TimetableService.Data.Models;
using UniversityHelper.TimetableService.Models.Dto;

namespace UniversityHelper.TimetableService.Mappers;

public static class GroupMapper
{
    public static GroupDto ToDto(this Group group)
    {
        if (group == null) return null;
        return new GroupDto
        {
            Id = group.Id,
            Institute = group.Institute,
            Faculty = group.Faculty,
            Course = group.Course,
            Subjects = group.Subjects?.Select(s => s.ToDto()).ToList()
        };
    }
} 