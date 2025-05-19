using FluentValidation;
using UniversityHelper.TimetableService.Data.Models;

namespace UniversityHelper.TimetableService.Business.Validators;

public class SubjectValidator : AbstractValidator<Subject>
{
    public SubjectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Subject name is required")
            .MaximumLength(200).WithMessage("Subject name must not exceed 200 characters");

        RuleFor(x => x.Teacher)
            .NotEmpty().WithMessage("Teacher name is required")
            .MaximumLength(100).WithMessage("Teacher name must not exceed 100 characters");

        RuleFor(x => x.Room)
            .NotEmpty().WithMessage("Room is required")
            .MaximumLength(50).WithMessage("Room must not exceed 50 characters");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start time is required");

        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End time is required")
            .GreaterThan(x => x.StartTime).WithMessage("End time must be after start time");

        RuleFor(x => x.GroupId)
            .NotEmpty().WithMessage("Group ID is required");

        RuleFor(x => x.TeacherId)
            .NotEmpty().WithMessage("Teacher ID is required");
    }
} 