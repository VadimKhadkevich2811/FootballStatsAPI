using AutoMapper;
using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Players.Queries.GetFreePlayers;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Handlers;

public class GetFreePlayersHandler : IRequestHandler<GetFreePlayersQuery, Response<PlayersListWithCountDto>>
{
    private readonly IPlayersRepository _repository;
    private readonly IMapper _mapper;

    public GetFreePlayersHandler(IPlayersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<PlayersListWithCountDto>> Handle(GetFreePlayersQuery request, CancellationToken cancellationToken)
    {
        var date = request.Date;
        var freePlayers = await _repository.GetFreePlayersByDateAsync(date);
        var freePlayersCount = await _repository.GetFreePlayersByDateCountAsync(date);
        var freePlayerDtos = _mapper.Map<List<PlayerReadDto>>(freePlayers);

        var freePlayersResult = new PlayersListWithCountDto(freePlayerDtos, freePlayersCount);
        var freePlayersResponse = new Response<PlayersListWithCountDto>(freePlayersResult, true);

        return freePlayersResponse;
    }
}