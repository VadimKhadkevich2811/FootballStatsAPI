using AutoMapper;
using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Players.Queries.GetPlayerById;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Handlers;

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

        return new Response<PlayerReadDto>(playerDto, true, null, playerDto == null
            ? $"No Player Found By Id = {request.PlayerId}"
            : null);
    }
}