using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.Interfaces;

public interface IPlayersRepository
{
    Task AddPlayer(Player player);
    void RemovePlayer(Player player);
    Task<Player> GetPlayerById(int playerId);
    Task<List<Player>> GetAllPlayers();
    void UpdatePlayer(Player player);
    Task<bool> SaveChangesAsync();
}