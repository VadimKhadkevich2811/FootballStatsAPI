using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Queries.GetPlayerById;

public class GetPlayerByIdQuery : IRequest<Response<PlayerReadDto>>
{
    public int PlayerId { get; }

    public GetPlayerByIdQuery(int playerId)
    {
        PlayerId = playerId;
    }
}