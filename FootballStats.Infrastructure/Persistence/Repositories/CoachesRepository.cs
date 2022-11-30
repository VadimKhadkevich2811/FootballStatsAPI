using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FootballStats.ApplicationModule.Common.Interfaces;

public class CoachesRepository : ICoachesRepository
{
    private readonly IApplicationDbContext _context;
    private ISortHelper<Coach> _sortHelper;

    public CoachesRepository(IApplicationDbContext context, ISortHelper<Coach> sortHelper)
    {
        _context = context;
        _sortHelper = sortHelper;
    }

    public async Task AddCoachAsync(Coach coach)
    {
        await _context.Coaches.AddAsync(coach);
    }

    public async Task<List<Coach>> GetAllCoachesAsync()
    {
        return await _context.Coaches.ToListAsync();
    }

    public async Task<List<Coach>> GetAllCoachesAsync(CoachesQueryStringParams coachesFilter)
    {
        var coaches = coachesFilter.Name == null && coachesFilter.LastName == null
            ? _context.Coaches.Skip((coachesFilter.PageNumber - 1) * coachesFilter.PageSize).Take(coachesFilter.PageSize)
            : _context.Coaches.Where(coach =>
                (coach.Lastname.ToLower() == coachesFilter.LastName!.ToLower() || string.IsNullOrEmpty(coachesFilter.LastName)) &&
                (coach.Name.ToLower() == coachesFilter.Name!.ToLower() || string.IsNullOrEmpty(coachesFilter.Name)))
                .Skip((coachesFilter.PageNumber - 1) * coachesFilter.PageSize).Take(coachesFilter.PageSize);

        return await _sortHelper.ApplySort(coaches, coachesFilter.OrderBy!).ToListAsync();
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