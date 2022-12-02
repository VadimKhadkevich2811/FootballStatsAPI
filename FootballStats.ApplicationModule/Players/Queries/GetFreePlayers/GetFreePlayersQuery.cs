using FootballStats.ApplicationModule.Common.DTOs.Players;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Queries.GetFreePlayers;

public class GetFreePlayersQuery : IRequest<PlayersListWithCountDTO>
{
    public DateTime Date { get; }
    public GetFreePlayersQuery(DateTime date)
    {
        Date = date;
    }
}