using AutoMapper;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Players.Commands.DeletePlayer;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Handlers;

public class DeletePlayerHandler : IRequestHandler<DeletePlayerCommand, bool>
{
    private readonly IPlayersRepository _repository;
    private readonly IMapper _mapper;

    public DeletePlayerHandler(IPlayersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetPlayerById(request.PlayerId);

        if (player == null)
        {
            return false;
        }

        _repository.RemovePlayer(player);
        return await _repository.SaveChangesAsync();
    }
}