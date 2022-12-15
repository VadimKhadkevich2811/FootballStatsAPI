using AutoMapper;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Commands.UpdatePlayer;
using FootballStats.Application.Players.Commands.UpdatePlayerDetail;
using MediatR;

namespace FootballStats.Application.Players.Handlers;

public class UpdatePlayerDetailHandler : IRequestHandler<UpdatePlayerDetailCommand, Response<bool>>
{
    private readonly IPlayersRepository _repository;
    private readonly IMapper _mapper;
    public UpdatePlayerDetailHandler(IPlayersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(UpdatePlayerDetailCommand request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetPlayerByIdAsync(request.PlayerId);

        if (player == null)
        {
            return new Response<bool>(false, false, "Error during updating a player",
                $"No players found with ID = {request.PlayerId}");
        }

        var playerToPatch = _mapper.Map<UpdatePlayerCommand>(player);
        request.Item.ApplyTo(playerToPatch);

        _mapper.Map(playerToPatch, player);

        _repository.UpdatePlayer(player);

        return new Response<bool>(await _repository.SaveChangesAsync(), true);
    }
}