using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.Filters;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Queries.GetAllPlayersQuery;

public class GetAllPlayersQuery : IRequest<PlayersListWithCountDTO>
{
    public PaginationFilter PaginationFilter { get; set; } = default!;
    public PlayersFilter? PlayersFilter { get; set; }
    public string LastName { get; set; } = default!;
    public GetAllPlayersQuery(PaginationFilter paginationFilter, PlayersFilter? playersFilter)
    {
        PaginationFilter = paginationFilter;
        PlayersFilter = playersFilter != null && (string.IsNullOrEmpty(playersFilter!.Name) && 
            string.IsNullOrEmpty(playersFilter!.LastName))
            ? null 
            : playersFilter;
    }
}