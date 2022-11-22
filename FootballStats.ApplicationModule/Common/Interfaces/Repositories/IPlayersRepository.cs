using FootballStats.ApplicationModule.Common.Filters;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;

namespace FootballStats.ApplicationModule.Common.Interfaces.Repositories;

public interface IPlayersRepository
{
    Task AddPlayerAsync(Player player);
    void RemovePlayer(Player player);
    Task<Player> GetPlayerByIdAsync(int playerId);
    Task<List<Player>> GetAllPlayersAsync();
    Task<List<Player>> GetAllPlayersAsync(int pageNumber, int pageSize, PlayersFilter? playersFilter = null);
    Task<int> GetAllPlayersCountAsync();
    void UpdatePlayer(Player player);
    Task<bool> ArePlayersOfValidPositionAsync(PositionGroup coachPosition, ICollection<int> playerIDs);
    Task<List<Player>> GetPlayersByPositionAsync(PositionGroup position);
    Task<bool> SaveChangesAsync();
}