using AutoMapper;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.ApplicationModule.Common.Players.Handlers;

public class UpdatePlayerHandler : IRequestHandler<UpdatePlayerCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public UpdatePlayerHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = await _context.Players.Where(player => player.Id == request.Id).FirstOrDefaultAsync();

        if (player == null)
        {
            return false;
        }

        _mapper.Map(request, player);

        _context.Players.Update(player);
        return await _context.SaveChangesAsync();
    }
}