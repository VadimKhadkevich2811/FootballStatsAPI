using FluentValidation;

namespace FootballStats.ApplicationModule.Coaches.Commands.UpdateCoach;

public class UpdateCoachCommandValidator : AbstractValidator<UpdateCoachCommand>
{
    public UpdateCoachCommandValidator()
    {
        RuleFor(field => field.CoachId)
            .GreaterThan(0).WithMessage("Coach Id should be greater than 0");

        RuleFor(field => field.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(field => field.Lastname)
            .NotEmpty().WithMessage("Lastname is required");

        RuleFor(field => field.Age)
            .GreaterThan(0).WithMessage("Age should be greater than 0");

        RuleFor(field => field.Position)
            .NotNull().WithMessage("Position should be set");
    }
}