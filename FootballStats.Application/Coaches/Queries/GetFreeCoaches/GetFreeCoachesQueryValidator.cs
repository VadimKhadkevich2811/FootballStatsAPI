using FluentValidation;
using FootballStats.Application.Common.Interfaces;

namespace FootballStats.Application.Coaches.Queries.GetFreeCoaches;

public class GetCoachByIdQueryValidator : AbstractValidator<GetFreeCoachesQuery>
{
    public GetCoachByIdQueryValidator(IValidationOptionsService validationOptionsService)
    {
        var trainingMinDate = validationOptionsService.GetTrainingValidationOptions().MinDate;

        RuleFor(field => field.Date)
            .NotNull().WithMessage("Training date is required")
            .GreaterThan(trainingMinDate).WithMessage($"Training date should be after {trainingMinDate}");
    }
}