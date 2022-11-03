using FootballStats.ApplicationModule.Common.Filters;
using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.Interfaces;

public interface IPlayersRepository
{
    Task AddPlayer(Player player);
    void RemovePlayer(Player player);
    Task<Player> GetPlayerById(int playerId);
    Task<List<Player>> GetAllPlayers();
    Task<List<Player>> GetAllPlayers(int pageNumber, int pageSize, PlayersFilter playersFilter = null);
    Task<int> GetAllPlayersCount();
    void UpdatePlayer(Player player);
    Task<bool> SaveChangesAsync();
}