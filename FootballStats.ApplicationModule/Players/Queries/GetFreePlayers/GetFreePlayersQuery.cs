using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Queries.GetFreePlayers;

public class GetFreePlayersQuery : IRequest<Response<PlayersListWithCountDto>>
{
    public DateTime Date { get; }
    public GetFreePlayersQuery(DateTime date)
    {
        Date = date;
    }
}