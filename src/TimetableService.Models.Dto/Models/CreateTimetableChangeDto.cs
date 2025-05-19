using System;

namespace UniversityHelper.TimetableService.Models.Dto;

public class CreateTimetableChangeDto
{
    public Guid SubjectId { get; set; }
    public Guid GroupId { get; set; }
    public DateTime OriginalDate { get; set; }
    public DateTime NewDate { get; set; }
    public string OriginalRoom { get; set; }
    public string NewRoom { get; set; }
    public string OriginalTeacher { get; set; }
    public string NewTeacher { get; set; }
    public string Reason { get; set; }
} 