using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.Interfaces;

public interface ITrainingsRepository
{
    Task AddTraining(Training player, ICollection<int> playerIDs);
    void RemoveTraining(Training player);
    Task<Training> GetTrainingById(int playerId);
    Task<List<Training>> GetAllTrainings();
    Task<List<Training>> GetAllTrainings(int pageNumber, int pageSize);
    Task<int> GetAllTrainingsCount();
    Task UpdateTraining(Training player, ICollection<int> playerIDs);
    Task<bool> SaveChangesAsync();
}