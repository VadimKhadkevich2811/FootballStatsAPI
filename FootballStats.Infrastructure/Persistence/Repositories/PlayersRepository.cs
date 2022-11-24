using FootballStats.ApplicationModule.Common.Filters;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;
using Microsoft.EntityFrameworkCore;

public class PlayersRepository : IPlayersRepository
{
    private readonly IApplicationDbContext _context;
    private ISortHelper<Player> _sortHelper;

    public PlayersRepository(IApplicationDbContext context, ISortHelper<Player> sortHelper)
    {
        _context = context;
        _sortHelper = sortHelper;
    }

    public async Task AddPlayerAsync(Player player)
    {
        await _context.Players.AddAsync(player);
    }

    public async Task<bool> ArePlayersOfValidPositionAsync(PositionGroup coachPosition, ICollection<int> playerIDs)
    {
        return await _context.Players.Where(player => playerIDs.Contains(player.Id)).AllAsync(player => player.Position == coachPosition);
    }

    public async Task<List<Player>> GetAllPlayersAsync()
    {
        return await _context.Players.ToListAsync();
    }

    public async Task<List<Player>> GetAllPlayersAsync(PlayersQueryStringParams playersFilter)
    {
        var players = playersFilter.Name == null && playersFilter.LastName == null
            ? _context.Players.Skip((playersFilter.PageNumber - 1) * playersFilter.PageSize).Take(playersFilter.PageSize)
            : _context.Players.Where(player =>
                (player.Lastname.ToLower() == playersFilter.LastName!.ToLower() || string.IsNullOrEmpty(playersFilter.LastName)) &&
                (player.Name.ToLower() == playersFilter.Name!.ToLower() || string.IsNullOrEmpty(playersFilter.Name)))
                .Skip((playersFilter.PageNumber - 1) * playersFilter.PageSize).Take(playersFilter.PageSize);

        return await _sortHelper.ApplySort(players, playersFilter.OrderBy!).ToListAsync();
    }

    public async Task<int> GetAllPlayersCountAsync()
    {
        return await _context.Players.CountAsync();
    }

    public async Task<Player?> GetPlayerByIdAsync(int playerId)
    {
        return await _context.Players.Where(player => player.Id == playerId).FirstOrDefaultAsync();
    }

    public async Task<List<Player>> GetPlayersByPositionAsync(PositionGroup position)
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