using System.Linq;
using UniversityHelper.TimetableService.Models.Db;
using UniversityHelper.TimetableService.Models.Dto;

namespace UniversityHelper.TimetableService.Mappers;

public static class GroupMapper
{
    public static GroupDto ToDto(this DbGroup group)
    {
        if (group == null)
        {
            return null;
        }

        return new GroupDto
        {
            Id = group.Id,
            Institute = group.Institute,
            Faculcity = group.Faculcity,
            Degree = group.Degree,
            FormEducation = group.FormEducation,
            Course = group.Course,
            Group = group.Group,
            Direction = group.Direction,
            SubGroup = group.SubGroup,
            Subjects = group.Subjects?.Select(s => s.ToDto()).ToList()
        };
    }
} 