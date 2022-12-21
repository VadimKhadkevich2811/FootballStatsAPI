using AutoMapper;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Commands.DeletePlayer;
using MediatR;

namespace FootballStats.Application.Players.Handlers;

public class DeletePlayerHandler : IRequestHandler<DeletePlayerCommand, Response<bool>>
{
    private readonly IPlayersRepository _repository;

    public DeletePlayerHandler(IPlayersRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<bool>> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetPlayerByIdAsync(request.PlayerId);

        if (player == null)
        {
            return new Response<bool>(false, false, "Error during deleting a player",
                $"No players found with ID = {request.PlayerId}");
        }

        _repository.RemovePlayer(player);

        return new Response<bool>(await _repository.SaveChangesAsync(), true);
    }
}