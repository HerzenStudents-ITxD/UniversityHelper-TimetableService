using System;

namespace UniversityHelper.TimetableService.Models.Dto;

#nullable enable

public class SubjectDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string FullName { get; set; }
    public required string Teacher { get; set; }
    public required string Room { get; set; }
    public required string Building { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public required string Type { get; set; } // лекция, практика, лабораторная
    public required string Subgroup { get; set; } // подгруппа
    public required string Comment { get; set; }
    public bool IsOnline { get; set; }
    public required string OnlineLink { get; set; }
    public required string Color { get; set; } // цвет для отображения в календаре
    public Guid GroupId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateSubjectDto
{
    public required string Name { get; set; }
    public required string FullName { get; set; }
    public required string Teacher { get; set; }
    public required string Room { get; set; }
    public required string Building { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public required string Type { get; set; }
    public required string Subgroup { get; set; }
    public required string Comment { get; set; }
    public bool IsOnline { get; set; }
    public required string OnlineLink { get; set; }
    public required string Color { get; set; }
    public Guid GroupId { get; set; }
}

public class UpdateSubjectDto
{
    public required string Name { get; set; }
    public required string FullName { get; set; }
    public required string Teacher { get; set; }
    public required string Room { get; set; }
    public required string Building { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public required string Type { get; set; }
    public required string Subgroup { get; set; }
    public required string Comment { get; set; }
    public bool IsOnline { get; set; }
    public required string OnlineLink { get; set; }
    public required string Color { get; set; }
} 