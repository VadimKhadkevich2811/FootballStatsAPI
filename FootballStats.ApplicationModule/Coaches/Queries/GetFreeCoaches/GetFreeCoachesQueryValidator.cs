using FluentValidation;

namespace FootballStats.ApplicationModule.Coaches.Queries.GetFreeCoaches;

public class GetCoachByIdQueryValidator : AbstractValidator<GetFreeCoachesQuery>
{
    public GetCoachByIdQueryValidator()
    {
        RuleFor(field => field.Date)
            .NotNull().WithMessage("Date is required");
    }
}