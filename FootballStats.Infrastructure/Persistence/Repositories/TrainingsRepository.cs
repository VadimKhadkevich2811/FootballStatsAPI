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

    public async Task AddTraining(Training training, ICollection<int> playerIDs)
    {
        await _context.Trainings.AddAsync(training);

        foreach (var pId in playerIDs)
        {
            _context.TrainingPlayers.Add(new() { PlayerId = pId, TrainingId = training.Id });
        }
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
        var neeededtrainingPlayers = _context.TrainingPlayers.Where(tp => tp.TrainingId == training.Id);
        
        foreach (var tp in neeededtrainingPlayers)
        {
            _context.TrainingPlayers.Remove(tp);
        }

        _context.Trainings.Remove(training);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task UpdateTraining(Training training, ICollection<int> playerIDs)
    {
        _context.Trainings.Update(training);

        var oldTrainingPlayers = _context.TrainingPlayers.Where(player => !playerIDs.Contains(player.PlayerId));
        var newTrainingPlayersIDs = playerIDs.Except(playerIDs.Where(pid => _context.TrainingPlayers.Any(tp => tp.PlayerId == pid)));

        foreach(var oldTP in oldTrainingPlayers)
        {
            _context.TrainingPlayers.Remove(oldTP);
        }

        foreach(var newTPId in newTrainingPlayersIDs)
        {
            await _context.TrainingPlayers.AddAsync(new(){PlayerId = newTPId, TrainingId = training.Id});
        }
    }
}