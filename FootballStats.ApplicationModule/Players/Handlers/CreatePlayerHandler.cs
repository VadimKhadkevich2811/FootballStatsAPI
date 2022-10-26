using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Players.Commands.CreatePlayer;
using FootballStats.Domain.Entities;
using MediatR;

namespace FootballStats.ApplicationModule.Common.Players.Handlers;

public class CreatePlayerHandler : IRequestHandler<CreatePlayerCommand, PlayerReadDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public CreatePlayerHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PlayerReadDTO> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = _mapper.Map<Player>(request);
        await _context.Players.AddAsync(player);
        await _context.SaveChangesAsync();

        var newPlayer = _mapper.Map<PlayerReadDTO>(player);

        return newPlayer;
    }
}