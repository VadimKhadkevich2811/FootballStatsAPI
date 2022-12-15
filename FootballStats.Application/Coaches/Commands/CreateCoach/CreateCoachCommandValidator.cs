using FluentValidation;
using FootballStats.Application.Common.Interfaces;

namespace FootballStats.Application.Coaches.Commands.CreateCoach;

public class CreateCoachCommandValidator : AbstractValidator<CreateCoachCommand>
{
    public CreateCoachCommandValidator(IValidationOptionsService validationOptionsService)
    {
        var coachMinAge = validationOptionsService.GetCoachValidationOptions().MinAge;
        var coachMaxAge = validationOptionsService.GetCoachValidationOptions().MaxAge;

        RuleFor(field => field.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(field => field.Lastname)
            .NotEmpty().WithMessage("Lastname is required");

        RuleFor(field => field.Age)
            .GreaterThan(coachMinAge).WithMessage($"Age should be greater than {coachMinAge}")
            .LessThan(coachMaxAge).WithMessage($"Age should be less than {coachMaxAge}");

        RuleFor(field => field.Position)
            .NotNull().WithMessage("Position should be set");
    }
}