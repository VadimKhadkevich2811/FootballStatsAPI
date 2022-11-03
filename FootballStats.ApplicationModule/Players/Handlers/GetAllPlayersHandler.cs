using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Players.Queries.GetAllPlayersQuery;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Handlers;

public class GetAllPlayersHandler : IRequestHandler<GetAllPlayersQuery, PlayersListWithCountDTO>
{
    private readonly IPlayersRepository _repository;
    private readonly IMapper _mapper;

    public GetAllPlayersHandler(IPlayersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PlayersListWithCountDTO> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
    {
        var paginationFilter = request.PaginationFilter;
        var playersFilter = request.PlayersFilter;
        var players = playersFilter == null
            ? await _repository.GetAllPlayers(paginationFilter.PageNumber, paginationFilter.PageSize)
            : await _repository.GetAllPlayers(paginationFilter.PageNumber, paginationFilter.PageSize, playersFilter);
        var playersCount = await _repository.GetAllPlayersCount();
        var playerDTOs = _mapper.Map<List<PlayerReadDTO>>(players);

        return new PlayersListWithCountDTO(playerDTOs, playersCount);
    }
}