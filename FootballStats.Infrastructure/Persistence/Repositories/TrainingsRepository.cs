using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;
using Microsoft.EntityFrameworkCore;

public class TrainingsRepository : ITrainingsRepository
{
    private readonly IApplicationDbContext _context;

    public TrainingsRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddTrainingAsync(Training training, ICollection<int> playerIDs)
    {
        await _context.Trainings.AddAsync(training);
        await SaveChangesAsync();
        foreach (var pId in playerIDs)
        {
            _context.TrainingPlayers.Add(new() { PlayerId = pId, TrainingId = training.Id });
        }
    }

    public async Task<List<Training>> GetAllTrainingsAsync()
    {
        return await _context.Trainings.ToListAsync();
    }

    public async Task<List<Training>> GetAllTrainingsAsync(int pageNumber, int pageSize)
    {
        return await _context.Trainings.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetAllTrainingsCountAsync()
    {
        return await _context.Trainings.CountAsync();
    }

    public async Task<Training?> GetTrainingByIdAsync(int trainingId)
    {
        return await _context.Trainings.Where(training => training.Id == trainingId).FirstOrDefaultAsync();
    }

    public async Task<List<Training>> GetTrainingsByCoachAsync(int coachId)
    {
        return await _context.Trainings.Where(training => training.CoachId == coachId).ToListAsync();
    }

    public async Task<List<Training>> GetTrainingsByPositionAsync(PositionGroup position)
    {
        return await (from training in _context.Trainings
                      join coach in _context.Coaches on training.CoachId equals coach.Id
                      where coach.Position == position
                      select training).ToListAsync();

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

    public async Task UpdateTrainingAsync(Training training, ICollection<int> playerIDs)
    {
        _context.Trainings.Update(training);

        if (playerIDs != null)
        {
            var oldTrainingPlayers = _context.TrainingPlayers.Where(player => !playerIDs.Contains(player.PlayerId));
            var newTrainingPlayersIDs = playerIDs.Except(playerIDs.Where(pid => _context.TrainingPlayers.Any(tp => tp.PlayerId == pid)));

            foreach (var oldTP in oldTrainingPlayers)
            {
                _context.TrainingPlayers.Remove(oldTP);
            }

            foreach (var newTPId in newTrainingPlayersIDs)
            {
                await _context.TrainingPlayers.AddAsync(new() { PlayerId = newTPId, TrainingId = training.Id });
            }
        }
    }
}