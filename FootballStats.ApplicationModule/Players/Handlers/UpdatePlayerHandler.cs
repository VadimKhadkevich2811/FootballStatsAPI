using AutoMapper;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;
using MediatR;

namespace FootballStats.ApplicationModule.Common.Players.Handlers;

public class UpdatePlayerHandler : IRequestHandler<UpdatePlayerCommand, Response<bool>>
{
    private readonly IPlayersRepository _repository;
    private readonly IMapper _mapper;
    public UpdatePlayerHandler(IPlayersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetPlayerByIdAsync(request.PlayerId);

        if (player == null)
        {
            return new Response<bool>(false, false, null, $"No players found with ID = {request.PlayerId}");
        }

        _mapper.Map(request, player);

        _repository.UpdatePlayer(player);

        return new Response<bool>(await _repository.SaveChangesAsync(), true);
    }
}