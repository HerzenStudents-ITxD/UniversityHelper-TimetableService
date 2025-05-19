using System;

namespace UniversityHelper.TimetableService.Models.Dto;

public class UpdateTimetableChangeDto
{
    public DateTime NewDate { get; set; }
    public string NewRoom { get; set; }
    public string NewTeacher { get; set; }
    public string Reason { get; set; }
} 