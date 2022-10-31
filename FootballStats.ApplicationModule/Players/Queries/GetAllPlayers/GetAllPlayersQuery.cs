using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.Filters;
using FootballStats.Domain.Entities;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Queries.GetAllPlayersQuery;

public class GetAllPlayersQuery : IRequest<PlayersListWithCountDTO>
{
    public PaginationFilter PlayersFilter { get; set; }
    public GetAllPlayersQuery(PaginationFilter filter)
    {
        PlayersFilter = filter;
    }
}