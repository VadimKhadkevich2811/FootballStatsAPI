using FluentValidation;

namespace FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;

public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerCommandValidator()
    {
        RuleFor(field => field.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(field => field.Lastname)
            .NotEmpty().WithMessage("Lastname is requiredF");

        RuleFor(field => field.Age)
            .GreaterThan(0).WithMessage("Age should be greater than 0");
    }
}