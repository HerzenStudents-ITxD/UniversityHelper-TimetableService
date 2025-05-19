using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using UniversityHelper.TimetableService.Business.Interfaces;
using UniversityHelper.TimetableService.Data;
using UniversityHelper.TimetableService.Data.Models;
using UniversityHelper.TimetableService.Models.Dto;
using UniversityHelper.TimetableService.Mappers;

namespace UniversityHelper.TimetableService.Business.Services;

public class TimetableService : ITimetableService
{
    private readonly TimetableDbContext _context;
    private readonly IMemoryCache _cache;
    private readonly ILogger<TimetableService> _logger;
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

    public TimetableService(
        TimetableDbContext context,
        IMemoryCache cache,
        ILogger<TimetableService> logger)
    {
        _context = context;
        _cache = cache;
        _logger = logger;
    }

    // Группы
    public async Task<GroupDto> GetGroupByIdAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Getting group by id: {GroupId}", id);
            Group group = await _context.Groups
                .Include(g => g.Subjects)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group == null)
            {
                _logger.LogWarning("Group not found: {GroupId}", id);
                return null;
            }

            _logger.LogInformation("Group found: {GroupId}", id);
            return group.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting group: {GroupId}", id);
            throw;
        }
    }

    public async Task<List<GroupDto>> GetAllGroupsAsync()
    {
        try
        {
            _logger.LogInformation("Getting all groups");
            List<Group> groups = await _context.Groups
                .Include(g => g.Subjects)
                .ToListAsync();
            _logger.LogInformation("Retrieved {Count} groups", groups.Count);
            return groups.Select(g => g.ToDto()).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all groups");
            throw;
        }
    }

    public async Task<GroupDto> CreateGroupAsync(CreateGroupDto groupDto)
    {
        try
        {
            _logger.LogInformation("Creating new group: {GroupName}", groupDto.Group);
            
            Group group = new Group
            {
                Institute = groupDto.Institute,
                Faculty = groupDto.Faculty,
                Degree = groupDto.Degree,
                FormEducation = groupDto.FormEducation,
                Course = groupDto.Course,
                GroupName = groupDto.Group,
                Name = groupDto.Group,
                Direction = groupDto.Direction,
                SubGroup = groupDto.SubGroup,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Group created successfully: {GroupId}", group.Id);
            
            return group.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating group: {GroupName}", groupDto.Group);
            throw;
        }
    }

    public async Task<GroupDto> UpdateGroupAsync(Guid id, UpdateGroupDto groupDto)
    {
        try
        {
            _logger.LogInformation("Updating group: {GroupId}", id);
            
            Group group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                _logger.LogWarning("Group not found for update: {GroupId}", id);
                return null;
            }

            group.Institute = groupDto.Institute;
            group.Faculty = groupDto.Faculty;
            group.Degree = groupDto.Degree;
            group.FormEducation = groupDto.FormEducation;
            group.Course = groupDto.Course;
            group.GroupName = groupDto.Group;
            group.Direction = groupDto.Direction;
            group.SubGroup = groupDto.SubGroup;
            group.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            _logger.LogInformation("Group updated successfully: {GroupId}", id);
            
            return group.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating group: {GroupId}", id);
            throw;
        }
    }

    public async Task DeleteGroupAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Deleting group: {GroupId}", id);
            
            Group group = await _context.Groups.FindAsync(id);
            if (group != null)
            {
                _context.Groups.Remove(group);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Group deleted successfully: {GroupId}", id);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting group: {GroupId}", id);
            throw;
        }
    }

    // Предметы
    public async Task<SubjectDto> GetSubjectByIdAsync(Guid id)
    {
        Subject subject = await _context.Subjects.FindAsync(id);
        return subject?.ToDto();
    }

    public async Task<List<SubjectDto>> GetSubjectsByGroupIdAsync(Guid groupId)
    {
        List<Subject> subjects = await _context.Subjects
            .Where(s => s.GroupId == groupId)
            .ToListAsync();

        return subjects.Select(s => s.ToDto()).ToList();
    }

    public async Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto subjectDto)
    {
        Subject subject = new Subject
        {
            GroupId = subjectDto.GroupId,
            Name = subjectDto.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();

        return subject.ToDto();
    }

    public async Task<SubjectDto> UpdateSubjectAsync(Guid id, UpdateSubjectDto subjectDto)
    {
        Subject subject = await _context.Subjects.FindAsync(id);
        if (subject == null)
        {
            return null;
        }

        subject.Name = subjectDto.Name;
        subject.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return subject.ToDto();
    }

    public async Task DeleteSubjectAsync(Guid id)
    {
        Subject subject = await _context.Subjects.FindAsync(id);
        if (subject != null)
        {
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }
    }

    // Расписание
    public async Task<PaginatedResult<SubjectDto>> GetTimetableForGroupAsync(
        Guid groupId,
        DateTime startDate,
        DateTime endDate,
        int pageNumber = 1,
        int pageSize = 20)
    {
        _logger.LogInformation("Getting timetable for group {GroupId} from {StartDate} to {EndDate}", 
            groupId, startDate, endDate);

        try
        {
            string cacheKey = $"timetable_group_{groupId}_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}_{pageNumber}_{pageSize}";

            if (_cache.TryGetValue(cacheKey, out PaginatedResult<SubjectDto> cachedResult))
            {
                return cachedResult;
            }

            IQueryable<Subject> query = _context.Subjects
                .Where(s => s.GroupId == groupId && s.StartTime >= startDate && s.EndTime <= endDate)
                .OrderBy(s => s.StartTime);

            int totalCount = await query.CountAsync();
            List<SubjectDto> items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => s.ToDto())
                .ToListAsync();

            PaginatedResult<SubjectDto> result = new PaginatedResult<SubjectDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            _cache.Set(cacheKey, result, _cacheDuration);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting timetable for group {GroupId}", groupId);
            throw;
        }
    }

    public async Task<List<TimetableChangeDto>> GetTimetableChangesAsync(
        Guid groupId,
        DateTime startDate,
        DateTime endDate)
    {
        _logger.LogInformation("Getting timetable changes for group {GroupId} from {StartDate} to {EndDate}",
            groupId, startDate, endDate);

        try
        {
            string cacheKey = $"timetable_changes_{groupId}_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}";

            if (_cache.TryGetValue(cacheKey, out List<TimetableChangeDto> cachedChanges))
            {
                return cachedChanges;
            }

            List<TimetableChangeDto> changes = await _context.TimetableChanges
                .Where(c => c.GroupId == groupId && c.NewDate >= startDate && c.NewDate <= endDate)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => c.ToDto())
                .ToListAsync();

            _cache.Set(cacheKey, changes, _cacheDuration);

            return changes;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting timetable changes for group {GroupId}", groupId);
            throw;
        }
    }

    public async Task<List<SubjectDto>> GetTeacherTimetableAsync(
        Guid teacherId,
        DateTime startDate,
        DateTime endDate)
    {
        _logger.LogInformation("Getting timetable for teacher {TeacherId} from {StartDate} to {EndDate}",
            teacherId, startDate, endDate);

        try
        {
            string cacheKey = $"timetable_teacher_{teacherId}_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}";

            if (_cache.TryGetValue(cacheKey, out List<SubjectDto> cachedTimetable))
            {
                return cachedTimetable;
            }

            List<SubjectDto> timetable = await _context.Subjects
                .Where(s => s.TeacherId == teacherId && s.StartTime >= startDate && s.EndTime <= endDate)
                .OrderBy(s => s.StartTime)
                .Select(s => s.ToDto())
                .ToListAsync();

            _cache.Set(cacheKey, timetable, _cacheDuration);

            return timetable;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting timetable for teacher {TeacherId}", teacherId);
            throw;
        }
    }

    public async Task<List<SubjectDto>> GetRoomTimetableAsync(
        string roomId,
        DateTime startDate,
        DateTime endDate)
    {
        _logger.LogInformation("Getting timetable for room {RoomId} from {StartDate} to {EndDate}",
            roomId, startDate, endDate);

        try
        {
            string cacheKey = $"timetable_room_{roomId}_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}";

            if (_cache.TryGetValue(cacheKey, out List<SubjectDto> cachedTimetable))
            {
                return cachedTimetable;
            }

            List<SubjectDto> timetable = await _context.Subjects
                .Where(s => s.Room == roomId && s.StartTime >= startDate && s.EndTime <= endDate)
                .OrderBy(s => s.StartTime)
                .Select(s => s.ToDto())
                .ToListAsync();

            _cache.Set(cacheKey, timetable, _cacheDuration);

            return timetable;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting timetable for room {RoomId}", roomId);
            throw;
        }
    }

    public async Task<TimetableChangeDto> CreateTimetableChangeAsync(CreateTimetableChangeDto changeDto)
    {
        _logger.LogInformation("Creating timetable change for subject {SubjectId}", changeDto.SubjectId);

        try
        {
            TimetableChange change = new TimetableChange
            {
                SubjectId = changeDto.SubjectId,
                GroupId = changeDto.GroupId,
                OriginalDate = changeDto.OriginalDate,
                NewDate = changeDto.NewDate,
                OriginalRoom = changeDto.OriginalRoom,
                NewRoom = changeDto.NewRoom,
                OriginalTeacher = changeDto.OriginalTeacher,
                NewTeacher = changeDto.NewTeacher,
                Reason = changeDto.Reason,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.TimetableChanges.Add(change);
            await _context.SaveChangesAsync();

            return change.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating timetable change for subject {SubjectId}", changeDto.SubjectId);
            throw;
        }
    }

    public async Task<TimetableChangeDto> UpdateTimetableChangeAsync(Guid id, UpdateTimetableChangeDto changeDto)
    {
        _logger.LogInformation("Updating timetable change {ChangeId}", id);

        try
        {
            TimetableChange change = await _context.TimetableChanges.FindAsync(id);
            if (change == null)
            {
                return null;
            }

            change.NewDate = changeDto.NewDate;
            change.NewRoom = changeDto.NewRoom;
            change.NewTeacher = changeDto.NewTeacher;
            change.Reason = changeDto.Reason;
            change.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return change.ToDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating timetable change {ChangeId}", id);
            throw;
        }
    }

    public async Task DeleteTimetableChangeAsync(Guid id)
    {
        _logger.LogInformation("Deleting timetable change {ChangeId}", id);

        try
        {
            TimetableChange change = await _context.TimetableChanges.FindAsync(id);
            if (change != null)
            {
                _context.TimetableChanges.Remove(change);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting timetable change {ChangeId}", id);
            throw;
        }
    }
} 