using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;

namespace FootballStats.ApplicationModule.Common.Interfaces.Repositories;

public interface ITrainingsRepository
{
    Task AddTraining(Training player, ICollection<int> playerIDs);
    void RemoveTraining(Training player);
    Task<Training> GetTrainingById(int playerId);
    Task<List<Training>> GetAllTrainings();
    Task<List<Training>> GetAllTrainings(int pageNumber, int pageSize);
    Task<int> GetAllTrainingsCount();
    Task UpdateTraining(Training player, ICollection<int> playerIDs);
    Task<List<Training>> GetTrainingsByPosition(PositionGroup position);
    Task<List<Training>> GetTrainingsByCoach(int coachId);
    Task<bool> SaveChangesAsync();
}