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
    Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto subject);
    Task<SubjectDto> UpdateSubjectAsync(Guid id, UpdateSubjectDto subject);
    Task DeleteSubjectAsync(Guid id);

    // Расписание
    Task<PaginatedResult<SubjectDto>> GetTimetableForGroupAsync(
        Guid groupId,
        DateTime startDate,
        DateTime endDate,
        int pageNumber = 1,
        int pageSize = 20);

    Task<List<SubjectDto>> GetTeacherTimetableAsync(
        Guid teacherId,
        DateTime startDate,
        DateTime endDate);

    Task<List<SubjectDto>> GetRoomTimetableAsync(
        string roomId,
        DateTime startDate,
        DateTime endDate);

    // Изменения в расписании
    Task<List<TimetableChangeDto>> GetTimetableChangesAsync(
        Guid groupId,
        DateTime startDate,
        DateTime endDate);

    Task<TimetableChangeDto> CreateTimetableChangeAsync(CreateTimetableChangeDto change);
    Task<TimetableChangeDto> UpdateTimetableChangeAsync(Guid id, UpdateTimetableChangeDto change);
    Task DeleteTimetableChangeAsync(Guid id);
} 