using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;

namespace FootballStats.ApplicationModule.Common.Interfaces.Repositories;

public interface ITrainingsRepository
{
    Task AddTrainingAsync(Training player, ICollection<int> playerIDs);
    void RemoveTraining(Training player);
    Task<Training> GetTrainingByIdAsync(int playerId);
    Task<List<Training>> GetAllTrainingsAsync();
    Task<List<Training>> GetAllTrainingsAsync(int pageNumber, int pageSize);
    Task<int> GetAllTrainingsCountAsync();
    Task UpdateTrainingAsync(Training player, ICollection<int> playerIDs);
    Task<List<Training>> GetTrainingsByPositionAsync(PositionGroup position);
    Task<List<Training>> GetTrainingsByCoachAsync(int coachId);
    Task<bool> SaveChangesAsync();
}