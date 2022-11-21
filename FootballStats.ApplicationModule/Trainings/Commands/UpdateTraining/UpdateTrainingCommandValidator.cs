using FluentValidation;

namespace FootballStats.ApplicationModule.Trainings.Commands.UpdateTraining;

public class UpdateTrainingCommandValidator : AbstractValidator<UpdateTrainingCommand>
{
    public UpdateTrainingCommandValidator()
    {
        RuleFor(field => field.Name)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(field => field.CoachId)
            .GreaterThan(0).WithMessage("CoachId should be greater than 0");

        RuleFor(field => field.TrainingId)
            .GreaterThan(0).WithMessage("Age should be greater than 0");

        RuleFor(field => field.PlayerIds)
            .Must(x => x.Any()).WithMessage("PlayerIDs should be set");
    }
}