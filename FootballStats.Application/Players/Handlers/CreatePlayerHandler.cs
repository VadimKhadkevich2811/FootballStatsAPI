using AutoMapper;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Commands.CreatePlayer;
using FootballStats.Application.Players.Dtos;
using FootballStats.Domain.Entities;
using MediatR;

namespace FootballStats.Application.Players.Handlers;

public class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, Response<PlayerReadDto>>
{
    private readonly IPlayersRepository _repository;
    private readonly IMapper _mapper;
    public CreatePlayerHandler(IPlayersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<PlayerReadDto>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = _mapper.Map<Player>(request);
        await _repository.AddPlayerAsync(player);
        await _repository.SaveChangesAsync();

        var newPlayer = _mapper.Map<PlayerReadDto>(player);

        var newPlayerResponse = new Response<PlayerReadDto>(newPlayer, true);

        return newPlayerResponse;
    }
}