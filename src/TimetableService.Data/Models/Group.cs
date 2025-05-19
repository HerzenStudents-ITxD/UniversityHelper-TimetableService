using System;
using System.Collections.Generic;

namespace UniversityHelper.TimetableService.Data.Models;

public class Group
{
    public Guid Id { get; set; }
    public string Institute { get; set; }
    public string Faculty { get; set; }
    public string Degree { get; set; }
    public string FormEducation { get; set; }
    public int Course { get; set; }
    public string GroupName { get; set; }
    public string Direction { get; set; }
    public string SubGroup { get; set; }
    public List<Subject> Subjects { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
} 