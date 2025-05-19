using System;

namespace UniversityHelper.TimetableService.Models.Dto;

public class TimetableChangeDto
{
    public Guid Id { get; set; }
    public Guid SubjectId { get; set; }
    public Guid GroupId { get; set; }
    public DateTime OriginalDate { get; set; }
    public DateTime NewDate { get; set; }
    public string OriginalRoom { get; set; }
    public string NewRoom { get; set; }
    public string OriginalTeacher { get; set; }
    public string NewTeacher { get; set; }
    public string Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
} 