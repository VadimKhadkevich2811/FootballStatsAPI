using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Players.Queries.GetPlayerById;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.ApplicationModule.Players.Handlers;

public class GetPlayerByIdHandler : IRequestHandler<GetPlayerByIdQuery, PlayerReadDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPlayerByIdHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PlayerReadDTO> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
    {
        var player = await _context.Players.Where(player => player.Id == request.PlayerId).FirstOrDefaultAsync();
        var playerDTO = _mapper.Map<PlayerReadDTO>(player);

        return playerDTO;
    }
}