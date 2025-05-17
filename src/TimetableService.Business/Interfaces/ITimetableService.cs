using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityHelper.TimetableService.Models.Dto;

namespace UniversityHelper.TimetableService.Business.Interfaces;

public interface ITimetableService
{
    // Группы
    Task<GroupDto> GetGroupByIdAsync(Guid id);
    Task<List<GroupDto>> GetAllGroupsAsync();
    Task<GroupDto> CreateGroupAsync(CreateGroupDto group);
    Task<GroupDto> UpdateGroupAsync(Guid id, UpdateGroupDto group);
    Task DeleteGroupAsync(Guid id);

    // Предметы
    Task<SubjectDto> GetSubjectByIdAsync(Guid id);
    Task<List<SubjectDto>> GetSubjectsByGroupIdAsync(Guid groupId);
    Task<List<SubjectDto>> GetSubjectsByPointIdAsync(Guid pointId);
    Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto subject);
    Task<SubjectDto> UpdateSubjectAsync(Guid id, UpdateSubjectDto subject);
    Task DeleteSubjectAsync(Guid id);

    // Расписание
    Task<List<SubjectDto>> GetTimetableForGroupAsync(Guid groupId, DateTime startDate, DateTime endDate);
    Task<List<SubjectDto>> GetTimetableForPointAsync(Guid pointId, DateTime startDate, DateTime endDate);
} 