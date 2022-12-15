using FootballStats.Application.Common.QueryParams;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using MediatR;

namespace FootballStats.Application.Players.Queries.GetAllPlayers;

public class GetAllPlayersQuery : IRequest<Response<PlayersListWithCountDto>>
{
    public PlayersQueryStringParams PlayersQueryStringParams { get; set; }
    public GetAllPlayersQuery(PlayersQueryStringParams playersQueryStringParams)
    {
        PlayersQueryStringParams = playersQueryStringParams;
    }
}