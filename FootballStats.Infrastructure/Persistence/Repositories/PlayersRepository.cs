using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class PlayersRepository : IPlayersRepository
{
    private readonly IApplicationDbContext _context;

    public PlayersRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddPlayer(Player player)
    {
        await _context.Players.AddAsync(player);
    }

    public async Task<List<Player>> GetAllPlayers()
    {
        return await _context.Players.ToListAsync();
    }

    public async Task<Player> GetPlayerById(int playerId)
    {
        return await _context.Players.Where(player => player.Id == playerId).FirstOrDefaultAsync();
    }

    public void RemovePlayer(Player player)
    {
        _context.Players.Remove(player);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void UpdatePlayer(Player player)
    {
        _context.Players.Update(player);
    }
}