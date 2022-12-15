using FluentValidation;

namespace FootballStats.Application.Players.Queries.GetFreePlayers;

public class GetFreePlayersQueryValidator : AbstractValidator<GetFreePlayersQuery>
{
    public GetFreePlayersQueryValidator()
    {
        RuleFor(field => field.Date)
            .NotNull().WithMessage("Date is required");
    }
}