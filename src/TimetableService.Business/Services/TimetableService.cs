using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniversityHelper.TimetableService.Business.Interfaces;
using UniversityHelper.TimetableService.Data.Provider;
using UniversityHelper.TimetableService.Models.Db;
using UniversityHelper.TimetableService.Models.Dto;
using UniversityHelper.TimetableService.Mappers;

namespace UniversityHelper.TimetableService.Business.Services;

public class TimetableService : ITimetableService
{
    private readonly TimetableDbContext _context;

    public TimetableService(TimetableDbContext context)
    {
        _context = context;
    }

    // Группы
    public async Task<GroupDto> GetGroupByIdAsync(Guid id)
    {
        DbGroup group = await _context.Groups
            .Include(g => g.Subjects)
            .FirstOrDefaultAsync(g => g.Id == id);

        return group?.ToDto();
    }

    public async Task<List<GroupDto>> GetAllGroupsAsync()
    {
        List<DbGroup> groups = await _context.Groups
            .Include(g => g.Subjects)
            .ToListAsync();

        return groups.Select(g => g.ToDto()).ToList();
    }

    public async Task<GroupDto> CreateGroupAsync(CreateGroupDto groupDto)
    {
        DbGroup group = new DbGroup
        {
            Institute = groupDto.Institute,
            Faculcity = groupDto.Faculcity,
            Degree = groupDto.Degree,
            FormEducation = groupDto.FormEducation,
            Course = groupDto.Course,
            Group = groupDto.Group,
            Direction = groupDto.Direction,
            SubGroup = groupDto.SubGroup,
            UpdateAt = DateTime.UtcNow
        };

        _context.Groups.Add(group);
        await _context.SaveChangesAsync();

        return group.ToDto();
    }

    public async Task<GroupDto> UpdateGroupAsync(Guid id, UpdateGroupDto groupDto)
    {
        DbGroup group = await _context.Groups.FindAsync(id);
        if (group == null)
        {
            return null;
        }

        group.Institute = groupDto.Institute;
        group.Faculcity = groupDto.Faculcity;
        group.Degree = groupDto.Degree;
        group.FormEducation = groupDto.FormEducation;
        group.Course = groupDto.Course;
        group.Group = groupDto.Group;
        group.Direction = groupDto.Direction;
        group.SubGroup = groupDto.SubGroup;
        group.UpdateAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return group.ToDto();
    }

    public async Task DeleteGroupAsync(Guid id)
    {
        DbGroup group = await _context.Groups.FindAsync(id);
        if (group != null)
        {
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }
    }

    // Предметы
    public async Task<SubjectDto> GetSubjectByIdAsync(Guid id)
    {
        DbSubject subject = await _context.Subjects.FindAsync(id);
        return subject?.ToDto();
    }

    public async Task<List<SubjectDto>> GetSubjectsByGroupIdAsync(Guid groupId)
    {
        List<DbSubject> subjects = await _context.Subjects
            .Where(s => s.GroupId == groupId)
            .ToListAsync();

        return subjects.Select(s => s.ToDto()).ToList();
    }

    public async Task<List<SubjectDto>> GetSubjectsByPointIdAsync(Guid pointId)
    {
        List<DbSubject> subjects = await _context.Subjects
            .Where(s => s.PointId == pointId)
            .ToListAsync();

        return subjects.Select(s => s.ToDto()).ToList();
    }

    public async Task<SubjectDto> CreateSubjectAsync(CreateSubjectDto subjectDto)
    {
        DbSubject subject = new DbSubject
        {
            GroupId = subjectDto.GroupId,
            Name = subjectDto.Name,
            Date = subjectDto.Date,
            Professor = subjectDto.Professor,
            PointId = subjectDto.PointId,
            Place = subjectDto.Place,
            UpdateAt = DateTime.UtcNow
        };

        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();

        return subject.ToDto();
    }

    public async Task<SubjectDto> UpdateSubjectAsync(Guid id, UpdateSubjectDto subjectDto)
    {
        DbSubject subject = await _context.Subjects.FindAsync(id);
        if (subject == null)
        {
            return null;
        }

        subject.Name = subjectDto.Name;
        subject.Date = subjectDto.Date;
        subject.Professor = subjectDto.Professor;
        subject.PointId = subjectDto.PointId;
        subject.Place = subjectDto.Place;
        subject.UpdateAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return subject.ToDto();
    }

    public async Task DeleteSubjectAsync(Guid id)
    {
        DbSubject subject = await _context.Subjects.FindAsync(id);
        if (subject != null)
        {
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }
    }

    // Расписание
    public async Task<List<SubjectDto>> GetTimetableForGroupAsync(Guid groupId, DateTime startDate, DateTime endDate)
    {
        List<DbSubject> subjects = await _context.Subjects
            .Where(s => s.GroupId == groupId && s.Date >= startDate && s.Date <= endDate)
            .OrderBy(s => s.Date)
            .ToListAsync();

        return subjects.Select(s => s.ToDto()).ToList();
    }

    public async Task<List<SubjectDto>> GetTimetableForPointAsync(Guid pointId, DateTime startDate, DateTime endDate)
    {
        List<DbSubject> subjects = await _context.Subjects
            .Where(s => s.PointId == pointId && s.Date >= startDate && s.Date <= endDate)
            .OrderBy(s => s.Date)
            .ToListAsync();

        return subjects.Select(s => s.ToDto()).ToList();
    }
} 