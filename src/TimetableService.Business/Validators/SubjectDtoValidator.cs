using FluentValidation;
using UniversityHelper.TimetableService.Models.Dto;

namespace UniversityHelper.TimetableService.Business.Validators;

public class SubjectDtoValidator : AbstractValidator<SubjectDto>
{
    public SubjectDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200)
            .WithMessage("Название предмета обязательно и не должно превышать 200 символов");

        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(500)
            .WithMessage("Полное название предмета обязательно и не должно превышать 500 символов");

        RuleFor(x => x.Teacher)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Имя преподавателя обязательно и не должно превышать 100 символов");

        RuleFor(x => x.Room)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Номер аудитории обязателен и не должен превышать 50 символов");

        RuleFor(x => x.Building)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Название здания обязательно и не должно превышать 100 символов");

        RuleFor(x => x.StartTime)
            .NotEmpty()
            .LessThan(x => x.EndTime)
            .WithMessage("Время начала должно быть раньше времени окончания");

        RuleFor(x => x.EndTime)
            .NotEmpty()
            .GreaterThan(x => x.StartTime)
            .WithMessage("Время окончания должно быть позже времени начала");

        RuleFor(x => x.Type)
            .NotEmpty()
            .Must(x => x == "лекция" || x == "практика" || x == "лабораторная")
            .WithMessage("Тип занятия должен быть: лекция, практика или лабораторная");

        RuleFor(x => x.Subgroup)
            .MaximumLength(10)
            .WithMessage("Номер подгруппы не должен превышать 10 символов");

        RuleFor(x => x.OnlineLink)
            .MaximumLength(500)
            .When(x => x.IsOnline)
            .WithMessage("Ссылка на онлайн-занятие не должна превышать 500 символов");

        RuleFor(x => x.Color)
            .NotEmpty()
            .Matches("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")
            .WithMessage("Цвет должен быть в формате HEX (например, #FF0000)");

        RuleFor(x => x.GroupId)
            .NotEmpty()
            .WithMessage("ID группы обязателен");
    }
} 