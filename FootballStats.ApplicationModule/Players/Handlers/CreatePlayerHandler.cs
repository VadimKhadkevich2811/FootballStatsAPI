using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Players.Commands.CreatePlayer;
using FootballStats.Domain.Entities;
using MediatR;

namespace FootballStats.ApplicationModule.Common.Players.Handlers;

public class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, PlayerReadDTO>
{
    private readonly IPlayersRepository _repository;
    private readonly IMapper _mapper;
    public CreatePlayerHandler(IPlayersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PlayerReadDTO> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = _mapper.Map<Player>(request);
        await _repository.AddPlayer(player);
        await _repository.SaveChangesAsync();

        var newPlayer = _mapper.Map<PlayerReadDTO>(player);

        return newPlayer;
    }
}