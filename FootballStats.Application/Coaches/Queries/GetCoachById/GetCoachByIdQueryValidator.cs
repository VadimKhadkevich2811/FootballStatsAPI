using FluentValidation;

namespace FootballStats.Application.Coaches.Queries.GetCoachById;

public class GetCoachByIdQueryValidator : AbstractValidator<GetCoachByIdQuery>
{
    public GetCoachByIdQueryValidator()
    {
        RuleFor(field => field.CoachId)
            .GreaterThan(0).WithMessage("Coach Id should be greater than 0");
    }
}