using MediatR;

namespace FootballStats.ApplicationModule.Players.Commands.DeletePlayer;

public class DeletePlayerCommand : IRequest<bool>
{
    public int PlayerId { get; }

    public DeletePlayerCommand(int playerId)
    {
        PlayerId = playerId;
    }
}