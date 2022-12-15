using AutoMapper;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using FootballStats.Application.Players.Queries.GetAllPlayers;
using MediatR;

namespace FootballStats.Application.Players.Handlers;

public class GetAllPlayersHandler : IRequestHandler<GetAllPlayersQuery, Response<PlayersListWithCountDto>>
{
    private readonly IPlayersRepository _repository;
    private readonly IMapper _mapper;

    public GetAllPlayersHandler(IPlayersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<PlayersListWithCountDto>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
    {
        var filter = request.PlayersQueryStringParams;
        var players = await _repository.GetAllPlayersAsync(filter);
        var playersCount = await _repository.GetAllPlayersCountAsync();
        var playerDtos = _mapper.Map<List<PlayerReadDto>>(players);
        var playersResult = new PlayersListWithCountDto(playerDtos, playersCount);
        var playersResponse = new Response<PlayersListWithCountDto>(playersResult, true);

        return playersResponse;
    }
}