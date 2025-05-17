using System;
using System.Collections.Generic;

namespace UniversityHelper.TimetableService.Models.Dto;

public class GroupDto
{
    public Guid Id { get; set; }
    public string Institute { get; set; }
    public string Faculcity { get; set; }
    public string Degree { get; set; }
    public string FormEducation { get; set; }
    public int Course { get; set; }
    public string Group { get; set; }
    public string Direction { get; set; }
    public string SubGroup { get; set; }
    public List<SubjectDto> Subjects { get; set; }
}

public class CreateGroupDto
{
    public string Institute { get; set; }
    public string Faculcity { get; set; }
    public string Degree { get; set; }
    public string FormEducation { get; set; }
    public int Course { get; set; }
    public string Group { get; set; }
    public string Direction { get; set; }
    public string SubGroup { get; set; }
}

public class UpdateGroupDto
{
    public string Institute { get; set; }
    public string Faculcity { get; set; }
    public string Degree { get; set; }
    public string FormEducation { get; set; }
    public int Course { get; set; }
    public string Group { get; set; }
    public string Direction { get; set; }
    public string SubGroup { get; set; }
} 