using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.Domain.Entities;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Queries.GetPlayerById;

public class GetPlayerByIdQuery : IRequest<PlayerReadDTO>
{
    public int PlayerId { get; }

    public GetPlayerByIdQuery(int playerId)
    {
        PlayerId = playerId;
    }
}