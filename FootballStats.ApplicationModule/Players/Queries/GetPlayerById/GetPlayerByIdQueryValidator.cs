using FluentValidation;

namespace FootballStats.ApplicationModule.Players.Queries.GetPlayerById;

public class GetPlayerByIdQueryValidator : AbstractValidator<GetPlayerByIdQuery>
{
    public GetPlayerByIdQueryValidator()
    {
        RuleFor(field => field.PlayerId)
            .GreaterThan(0).WithMessage("UserId should be greater than 0");
    }
}