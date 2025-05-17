using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityHelper.TimetableService.Business.Interfaces;
using UniversityHelper.TimetableService.Models.Dto;

namespace UniversityHelper.TimetableService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimetableController : ControllerBase
{
    private readonly ITimetableService _timetableService;

    public TimetableController(ITimetableService timetableService)
    {
        _timetableService = timetableService;
    }

    // Группы
    [HttpGet("groups")]
    public async Task<ActionResult<List<GroupDto>>> GetAllGroups()
    {
        List<GroupDto> groups = await _timetableService.GetAllGroupsAsync();
        return Ok(groups);
    }

    [HttpGet("groups/{id}")]
    public async Task<ActionResult<GroupDto>> GetGroupById(Guid id)
    {
        GroupDto group = await _timetableService.GetGroupByIdAsync(id);
        if (group == null)
        {
            return NotFound();
        }

        return Ok(group);
    }

    [HttpPost("groups")]
    public async Task<ActionResult<GroupDto>> CreateGroup(CreateGroupDto group)
    {
        GroupDto createdGroup = await _timetableService.CreateGroupAsync(group);
        return CreatedAtAction(nameof(GetGroupById), new { id = createdGroup.Id }, createdGroup);
    }

    [HttpPut("groups/{id}")]
    public async Task<ActionResult<GroupDto>> UpdateGroup(Guid id, UpdateGroupDto group)
    {
        GroupDto updatedGroup = await _timetableService.UpdateGroupAsync(id, group);
        if (updatedGroup == null)
        {
            return NotFound();
        }

        return Ok(updatedGroup);
    }

    [HttpDelete("groups/{id}")]
    public async Task<IActionResult> DeleteGroup(Guid id)
    {
        await _timetableService.DeleteGroupAsync(id);
        return NoContent();
    }

    // Предметы
    [HttpGet("subjects/{id}")]
    public async Task<ActionResult<SubjectDto>> GetSubjectById(Guid id)
    {
        SubjectDto subject = await _timetableService.GetSubjectByIdAsync(id);
        if (subject == null)
        {
            return NotFound();
        }

        return Ok(subject);
    }

    [HttpGet("groups/{groupId}/subjects")]
    public async Task<ActionResult<List<SubjectDto>>> GetSubjectsByGroupId(Guid groupId)
    {
        List<SubjectDto> subjects = await _timetableService.GetSubjectsByGroupIdAsync(groupId);
        return Ok(subjects);
    }

    [HttpGet("points/{pointId}/subjects")]
    public async Task<ActionResult<List<SubjectDto>>> GetSubjectsByPointId(Guid pointId)
    {
        List<SubjectDto> subjects = await _timetableService.GetSubjectsByPointIdAsync(pointId);
        return Ok(subjects);
    }

    [HttpPost("subjects")]
    public async Task<ActionResult<SubjectDto>> CreateSubject(CreateSubjectDto subject)
    {
        SubjectDto createdSubject = await _timetableService.CreateSubjectAsync(subject);
        return CreatedAtAction(nameof(GetSubjectById), new { id = createdSubject.Id }, createdSubject);
    }

    [HttpPut("subjects/{id}")]
    public async Task<ActionResult<SubjectDto>> UpdateSubject(Guid id, UpdateSubjectDto subject)
    {
        SubjectDto updatedSubject = await _timetableService.UpdateSubjectAsync(id, subject);
        if (updatedSubject == null)
        {
            return NotFound();
        }

        return Ok(updatedSubject);
    }

    [HttpDelete("subjects/{id}")]
    public async Task<IActionResult> DeleteSubject(Guid id)
    {
        await _timetableService.DeleteSubjectAsync(id);
        return NoContent();
    }

    // Расписание
    [HttpGet("groups/{groupId}/timetable")]
    public async Task<ActionResult<List<SubjectDto>>> GetTimetableForGroup(
        Guid groupId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        List<SubjectDto> timetable = await _timetableService.GetTimetableForGroupAsync(groupId, startDate, endDate);
        return Ok(timetable);
    }

    [HttpGet("points/{pointId}/timetable")]
    public async Task<ActionResult<List<SubjectDto>>> GetTimetableForPoint(
        Guid pointId,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        List<SubjectDto> timetable = await _timetableService.GetTimetableForPointAsync(pointId, startDate, endDate);
        return Ok(timetable);
    }
} 