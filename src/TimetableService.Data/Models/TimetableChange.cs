using System;

namespace UniversityHelper.TimetableService.Data.Models;

public class TimetableChange
{
    public Guid Id { get; set; }
    public Guid SubjectId { get; set; }
    public Subject Subject { get; set; }
    public Guid GroupId { get; set; }
    public Group Group { get; set; }
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