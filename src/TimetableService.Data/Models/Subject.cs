using System;

namespace UniversityHelper.TimetableService.Data.Models;

public class Subject
{
    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public Group Group { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Teacher { get; set; }
    public string Room { get; set; }
    public string Building { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Type { get; set; }
    public string Subgroup { get; set; }
    public string Comment { get; set; }
    public bool IsOnline { get; set; }
    public string OnlineLink { get; set; }
    public string Color { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid TeacherId { get; set; }
} 