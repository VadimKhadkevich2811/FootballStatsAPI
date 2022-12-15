using FluentValidation;

namespace FootballStats.ApplicationModule.Trainings.Commands.CreateTraining;

public class CreateTrainingCommandValidator : AbstractValidator<CreateTrainingCommand>
{
    public CreateTrainingCommandValidator()
    {
        RuleFor(field => field.Name)
            .NotEmpty().WithMessage("Name is required");
        RuleFor(field => field.CoachId)
            .GreaterThan(0).WithMessage("CoachId is required");
        RuleFor(field => field.PlayerIDs)
            .Must(x => x.Any()).WithMessage("PlayerIDs should be set");
        RuleFor(field => field.TrainingDate)
            .NotEmpty().WithMessage("Training Date is required");
    }
}