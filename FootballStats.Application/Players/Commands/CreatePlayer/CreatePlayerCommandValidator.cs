using FluentValidation;
using FootballStats.Application.Common.Interfaces;

namespace FootballStats.Application.Players.Commands.CreatePlayer;

public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerCommandValidator(IValidationOptionsService validationOptionsService)
    {
        var playerMinAge = validationOptionsService.GetPlayerValidationOptions().MinAge;
        var playerMaxAge = validationOptionsService.GetPlayerValidationOptions().MaxAge;

        RuleFor(field => field.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(field => field.Lastname)
            .NotEmpty().WithMessage("Lastname is required");

        RuleFor(field => field.Age)
            .GreaterThan(playerMinAge).WithMessage($"Age should be greater than {playerMinAge}")
            .LessThan(playerMaxAge).WithMessage($"Age should be less than {playerMaxAge}");

        RuleFor(field => field.Nationality)
            .NotEmpty().WithMessage("Nationality is required");

        RuleFor(field => field.Position)
            .NotNull().WithMessage("Position is required");
    }
}