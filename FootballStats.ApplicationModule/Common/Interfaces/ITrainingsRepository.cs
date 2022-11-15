using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.Interfaces;

public interface ITrainingsRepository
{
    Task AddTraining(Training player);
    void RemoveTraining(Training player);
    Task<Training> GetTrainingById(int playerId);
    Task<List<Training>> GetAllTrainings();
    Task<List<Training>> GetAllTrainings(int pageNumber, int pageSize);
    Task<int> GetAllTrainingsCount();
    void UpdateTraining(Training player);
    Task<bool> SaveChangesAsync();
}