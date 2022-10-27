using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Players.Queries.GetAllPlayersQuery;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.ApplicationModule.Players.Handlers;

public class GetAllPlayersHandler : IRequestHandler<GetAllPlayersQuery, List<PlayerReadDTO>>
{
    private readonly IPlayersRepository _repository;
    private readonly IMapper _mapper;

    public GetAllPlayersHandler(IPlayersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<PlayerReadDTO>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
    {
        var players = await _repository.GetAllPlayers();
        var playerDTOs = _mapper.Map<List<PlayerReadDTO>>(players);

        return playerDTOs;
    }
}