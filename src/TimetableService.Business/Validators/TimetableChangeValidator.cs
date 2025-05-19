using FluentValidation;
using UniversityHelper.TimetableService.Data.Models;

namespace UniversityHelper.TimetableService.Business.Validators;

public class TimetableChangeValidator : AbstractValidator<TimetableChange>
{
    public TimetableChangeValidator()
    {
        RuleFor(x => x.SubjectId)
            .NotEmpty().WithMessage("Subject ID is required");

        RuleFor(x => x.GroupId)
            .NotEmpty().WithMessage("Group ID is required");

        RuleFor(x => x.OriginalDate)
            .NotEmpty().WithMessage("Original date is required");

        RuleFor(x => x.NewDate)
            .NotEmpty().WithMessage("New date is required")
            .GreaterThan(x => x.OriginalDate).WithMessage("New date must be after original date");

        RuleFor(x => x.OriginalRoom)
            .NotEmpty().WithMessage("Original room is required")
            .MaximumLength(50).WithMessage("Original room must not exceed 50 characters");

        RuleFor(x => x.NewRoom)
            .NotEmpty().WithMessage("New room is required")
            .MaximumLength(50).WithMessage("New room must not exceed 50 characters");

        RuleFor(x => x.OriginalTeacher)
            .NotEmpty().WithMessage("Original teacher is required")
            .MaximumLength(100).WithMessage("Original teacher must not exceed 100 characters");

        RuleFor(x => x.NewTeacher)
            .NotEmpty().WithMessage("New teacher is required")
            .MaximumLength(100).WithMessage("New teacher must not exceed 100 characters");

        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("Reason is required")
            .MaximumLength(500).WithMessage("Reason must not exceed 500 characters");
    }
} 