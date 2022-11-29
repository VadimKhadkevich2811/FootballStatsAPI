using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Players.Queries.GetFreePlayers;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Handlers;

public class GetFreePlayersHandler : IRequestHandler<GetFreePlayersQuery, PlayersListWithCountDTO>
{
    private readonly IPlayersRepository _repository;
    private readonly IMapper _mapper;

    public GetFreePlayersHandler(IPlayersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PlayersListWithCountDTO> Handle(GetFreePlayersQuery request, CancellationToken cancellationToken)
    {
        var filter = request.PlayersQueryStringParams;
        var freePlayers = await _repository.GetFreePlayersAsync(filter);
        var freePlayersCount = await _repository.GetFreePlayersCountAsync();
        var freePlayerDTOs = _mapper.Map<List<PlayerReadDTO>>(freePlayers);

        return new PlayersListWithCountDTO(freePlayerDTOs, freePlayersCount);
    }
}