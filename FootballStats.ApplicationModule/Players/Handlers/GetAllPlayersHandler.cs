using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
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
        var filter = request.PlayersQueryStringParams;
        var players = await _repository.GetAllPlayersAsync(filter);
        var playersCount = await _repository.GetAllPlayersCountAsync();
        var playerDTOs = _mapper.Map<List<PlayerReadDTO>>(players);

        return new PlayersListWithCountDTO(playerDTOs, playersCount);
    }
}