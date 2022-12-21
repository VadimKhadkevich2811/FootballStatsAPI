using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using MediatR;

namespace FootballStats.Application.Players.Queries.GetPlayerById;

public class GetPlayerByIdQuery : IRequest<Response<PlayerReadDto>>
{
    public int PlayerId { get; }

    public GetPlayerByIdQuery(int playerId)
    {
        PlayerId = playerId;
    }
}