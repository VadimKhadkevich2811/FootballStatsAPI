using AutoMapper;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayerDetail;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.ApplicationModule.Common.Players.Handlers;

public class UpdatePlayerDetailHandler : IRequestHandler<UpdatePlayerDetailCommand, bool>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;
    public UpdatePlayerDetailHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdatePlayerDetailCommand request, CancellationToken cancellationToken)
    {
        var player = await _context.Players.Where(player => player.Id == request.Id).FirstOrDefaultAsync();

        if (player == null)
        {
            return false;
        }

        var playerToPatch = _mapper.Map<UpdatePlayerCommand>(player);
        request.Item.ApplyTo(playerToPatch);

        _mapper.Map(playerToPatch, player);

        _context.Players.Update(player);
        return await _context.SaveChangesAsync();
    }
}