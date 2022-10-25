using AutoMapper;
using FootballStats.ApplicationModule.Players.Commands.DeletePlayer;
using FootballStats.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.ApplicationModule.Players.Handlers;

public class DeletePlayerHandler : IRequestHandler<DeletePlayerCommand, bool>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public DeletePlayerHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        Player player;
        try
        {
            player = await _context.Players.Where(player => player.Id == request.PlayerId).FirstAsync();
        }
        catch
        {
            return false;
        }

        _context.Players.Remove(player);
        return await _context.SaveChangesAsync();
    }
}