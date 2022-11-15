using FluentValidation;

namespace FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;

public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerCommandValidator()
    {
        RuleFor(field => field.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(field => field.Lastname)
            .NotEmpty().WithMessage("Lastname is required");

        RuleFor(field => field.Age)
            .GreaterThan(0).WithMessage("Age should be greater than 0");
        
        RuleFor(field => field.Position)
            .NotEmpty().WithMessage("Position is required");
    }
}