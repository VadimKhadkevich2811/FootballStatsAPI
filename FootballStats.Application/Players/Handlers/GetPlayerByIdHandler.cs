using AutoMapper;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using FootballStats.Application.Players.Queries.GetPlayerById;
using MediatR;

namespace FootballStats.Application.Players.Handlers;

public class GetPlayerByIdHandler : IRequestHandler<GetPlayerByIdQuery, Response<PlayerReadDto>>
{
    private readonly IPlayersRepository _repository;
    private readonly IMapper _mapper;

    public GetPlayerByIdHandler(IPlayersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<PlayerReadDto>> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetPlayerByIdAsync(request.PlayerId);
        var playerDto = _mapper.Map<PlayerReadDto>(player);

        return playerDto == null
            ? new Response<PlayerReadDto>(null, false, "Error during getting a player by id",
                $"No Plauer Found By Id = {request.PlayerId}")
            : new Response<PlayerReadDto>(playerDto);
    }
}