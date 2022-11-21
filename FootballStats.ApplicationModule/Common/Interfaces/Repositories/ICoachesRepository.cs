using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.Interfaces.Repositories;

public interface ICoachesRepository
{
    Task AddCoachAsync(Coach coach);
    void RemoveCoach(Coach coach);
    Task<Coach> GetCoachByIdAsync(int coachId);
    Task<List<Coach>> GetAllCoachesAsync();
    Task<List<Coach>> GetAllCoachesAsync(int pageNumber, int pageSize);
    Task<int> GetAllCoachesCountAsync();
    void UpdateCoach(Coach coach);
    Task<bool> SaveChangesAsync();
}