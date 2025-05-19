using FluentValidation;
using UniversityHelper.TimetableService.Data.Models;

namespace UniversityHelper.TimetableService.Business.Validators;

public class GroupValidator : AbstractValidator<Group>
{
    public GroupValidator()
    {
        RuleFor(x => x.GroupName)
            .NotEmpty().WithMessage("Group name is required")
            .MaximumLength(100).WithMessage("Group name must not exceed 100 characters");

        RuleFor(x => x.Institute)
            .NotEmpty().WithMessage("Institute is required")
            .MaximumLength(200).WithMessage("Institute name must not exceed 200 characters");

        RuleFor(x => x.Faculty)
            .NotEmpty().WithMessage("Faculty is required")
            .MaximumLength(200).WithMessage("Faculty name must not exceed 200 characters");

        RuleFor(x => x.Degree)
            .NotEmpty().WithMessage("Degree is required")
            .MaximumLength(100).WithMessage("Degree must not exceed 100 characters");

        RuleFor(x => x.FormEducation)
            .NotEmpty().WithMessage("Form of education is required")
            .MaximumLength(100).WithMessage("Form of education must not exceed 100 characters");

        RuleFor(x => x.Course)
            .GreaterThan(0).WithMessage("Course must be greater than 0")
            .LessThanOrEqualTo(6).WithMessage("Course must not exceed 6");

        RuleFor(x => x.Direction)
            .NotEmpty().WithMessage("Direction is required")
            .MaximumLength(200).WithMessage("Direction must not exceed 200 characters");

        RuleFor(x => x.SubGroup)
            .MaximumLength(50).WithMessage("Subgroup must not exceed 50 characters");
    }
} 