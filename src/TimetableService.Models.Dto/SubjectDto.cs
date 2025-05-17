using System;

namespace UniversityHelper.TimetableService.Models.Dto;

public class SubjectDto
{
    public Guid Id { get; set; }
    public Guid GroupId { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Professor { get; set; }
    public Guid? PointId { get; set; }
    public string Place { get; set; }
}

public class CreateSubjectDto
{
    public Guid GroupId { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Professor { get; set; }
    public Guid? PointId { get; set; }
    public string Place { get; set; }
}

public class UpdateSubjectDto
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Professor { get; set; }
    public Guid? PointId { get; set; }
    public string Place { get; set; }
} 