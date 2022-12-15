using FluentValidation;

namespace FootballStats.Application.Trainings.Queries.GetTrainingById;

public class GetTrainingByIdQueryValidator : AbstractValidator<GetTrainingByIdQuery>
{
    public GetTrainingByIdQueryValidator()
    {
        RuleFor(field => field.TrainingId)
            .GreaterThan(0).WithMessage("Training Id should be greater than 0");
    }
}