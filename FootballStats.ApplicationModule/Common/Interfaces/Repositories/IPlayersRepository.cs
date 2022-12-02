using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;

namespace FootballStats.ApplicationModule.Common.Interfaces.Repositories;

public interface IPlayersRepository
{
    Task AddPlayerAsync(Player player);
    void RemovePlayer(Player player);
    Task<Player?> GetPlayerByIdAsync(int playerId);
    Task<List<Player>> GetAllPlayersAsync();
    Task<List<Player>> GetAllPlayersAsync(PlayersQueryStringParams playersFilter);
    Task<int> GetAllPlayersCountAsync();
    void UpdatePlayer(Player player);
    Task<bool> ArePlayersOfValidPositionAsync(PositionGroup coachPosition, ICollection<int> playerIDs);
    Task<List<Player>> GetPlayersByPositionAsync(PositionGroup position);
    Task<List<Player>> GetFreePlayersByDateAsync(DateTime date);
    Task<int> GetFreePlayersByDateCountAsync(DateTime date);
    Task<bool> SaveChangesAsync();
}