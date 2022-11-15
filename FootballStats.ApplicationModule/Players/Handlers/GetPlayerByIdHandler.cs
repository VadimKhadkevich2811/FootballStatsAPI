using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Players.Queries.GetPlayerById;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Handlers;

public class GetPlayerByIdHandler : IRequestHandler<GetPlayerByIdQuery, PlayerReadDTO>
{
    private readonly IPlayersRepository _repository;
    private readonly IMapper _mapper;

    public GetPlayerByIdHandler(IPlayersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PlayerReadDTO> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetPlayerById(request.PlayerId);
        var playerDTO = _mapper.Map<PlayerReadDTO>(player);

        return playerDTO;
    }
}