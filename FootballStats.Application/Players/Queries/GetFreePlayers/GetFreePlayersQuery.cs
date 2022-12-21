using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using MediatR;

namespace FootballStats.Application.Players.Queries.GetFreePlayers;

public class GetFreePlayersQuery : IRequest<Response<PlayersListWithCountDto>>
{
    public DateTime Date { get; }
    public GetFreePlayersQuery(DateTime date)
    {
        Date = date;
    }
}