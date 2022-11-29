using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class CoachesRepository : ICoachesRepository
{
    private readonly IApplicationDbContext _context;

    public CoachesRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddCoachAsync(Coach coach)
    {
        await _context.Coaches.AddAsync(coach);
    }

    public async Task<List<Coach>> GetAllCoachesAsync()
    {
        return await _context.Coaches.ToListAsync();
    }

    public async Task<List<Coach>> GetAllCoachesAsync(int pageNumber, int pageSize)
    {
        return await _context.Coaches.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetAllCoachesCountAsync()
    {
        return await _context.Coaches.CountAsync();
    }

    public async Task<Coach?> GetCoachByIdAsync(int coachId)
    {
        return await _context.Coaches.Where(coach => coach.Id == coachId).FirstOrDefaultAsync();
    }

    public void RemoveCoach(Coach coach)
    {
        _context.Coaches.Remove(coach);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void UpdateCoach(Coach coach)
    {
        _context.Coaches.Update(coach);
    }
}