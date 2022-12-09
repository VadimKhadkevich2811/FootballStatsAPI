using AutoMapper;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Players.Commands.DeletePlayer;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Handlers;

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
            return new Response<bool>(false, false, null, $"No players found with ID = {request.PlayerId}");
        }

        _repository.RemovePlayer(player);

        return new Response<bool>(await _repository.SaveChangesAsync(), true);
    }
}