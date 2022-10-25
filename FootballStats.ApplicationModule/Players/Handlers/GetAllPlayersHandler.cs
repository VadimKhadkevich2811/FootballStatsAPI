using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Players.Queries.GetAllPlayersQuery;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.ApplicationModule.Players.Handlers;

public class GetAllPlayersHandler : IRequestHandler<GetAllPlayersQuery, List<PlayerReadDTO>>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;

    public GetAllPlayersHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PlayerReadDTO>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
    {
        var players = await _context.Players.ToListAsync();
        var playerDTOs = _mapper.Map<List<PlayerReadDTO>>(players);

        return playerDTOs;
    }
}