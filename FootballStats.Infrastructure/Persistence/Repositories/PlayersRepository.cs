using FootballStats.ApplicationModule.Common.Filters;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;
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

    public async Task<bool> ArePlayersOfValidPosition(PositionGroup coachPosition)
    {
        return await _context.Players.AllAsync(player => player.Position == coachPosition);
    }

    public async Task<List<Player>> GetAllPlayers()
    {
        return await _context.Players.ToListAsync();
    }

    public async Task<List<Player>> GetAllPlayers(int pageNumber, int pageSize, PlayersFilter playersFilter = null)
    {
        return playersFilter == null
            ? await _context.Players.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
            : await _context.Players.Where(player =>
                (player.Lastname.ToLower() == playersFilter.LastName.ToLower() || string.IsNullOrEmpty(playersFilter.LastName)) &&
                (player.Name.ToLower() == playersFilter.Name.ToLower() || string.IsNullOrEmpty(playersFilter.Name)))
                .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetAllPlayersCount()
    {
        return await _context.Players.CountAsync();
    }

    public async Task<Player> GetPlayerById(int playerId)
    {
        return await _context.Players.Where(player => player.Id == playerId).FirstOrDefaultAsync();
    }

    public async Task<List<Player>> GetPlayersByPosition(PositionGroup position)
    {
        return await _context.Players.Where(player => player.Position == position).ToListAsync();
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