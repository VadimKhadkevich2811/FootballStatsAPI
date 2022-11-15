using FootballStats.ApplicationModule.Common.Filters;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class TrainingsRepository : ITrainingsRepository
{
    private readonly IApplicationDbContext _context;

    public TrainingsRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddTraining(Training training)
    {
        await _context.Trainings.AddAsync(training);
    }

    public async Task<List<Training>> GetAllTrainings()
    {
        return await _context.Trainings.ToListAsync();
    }

    public async Task<List<Training>> GetAllTrainings(int pageNumber, int pageSize)
    {
        return await _context.Trainings.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetAllTrainingsCount()
    {
        return await _context.Trainings.CountAsync();
    }

    public async Task<Training> GetTrainingById(int trainingId)
    {
        return await _context.Trainings.Where(training => training.Id == trainingId).FirstOrDefaultAsync();
    }

    public void RemoveTraining(Training training)
    {
        _context.Trainings.Remove(training);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void UpdateTraining(Training training)
    {
        _context.Trainings.Update(training);
    }
}