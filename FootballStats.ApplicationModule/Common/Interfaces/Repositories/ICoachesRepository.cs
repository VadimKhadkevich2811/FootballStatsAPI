using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.Interfaces.Repositories;

public interface ICoachesRepository
{
    Task AddCoach(Coach coach);
    void RemoveCoach(Coach coach);
    Task<Coach> GetCoachById(int coachId);
    Task<List<Coach>> GetAllCoaches();
    Task<List<Coach>> GetAllCoaches(int pageNumber, int pageSize);
    Task<int> GetAllCoachesCount();
    void UpdateCoach(Coach coach);
    Task<bool> SaveChangesAsync();
}