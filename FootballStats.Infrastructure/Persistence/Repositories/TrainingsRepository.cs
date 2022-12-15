using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.QueryParams;
using FootballStats.Domain.Entities;
using FootballStats.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.Infrastructure.Persistence.Repositories;

public class TrainingsRepository : ITrainingsRepository
{
    private readonly IApplicationDbContext _context;
    private readonly ISortHelper<Training> _sortHelper;

    public TrainingsRepository(IApplicationDbContext context, ISortHelper<Training> sortHelper)
    {
        _context = context;
        _sortHelper = sortHelper;
    }

    public async Task AddTrainingAsync(Training training, IEnumerable<int> playerIDs)
    {
        await _context.Trainings.AddAsync(training);
        await SaveChangesAsync();

        foreach (var pId in playerIDs)
        {
            await _context.TrainingPlayers.AddAsync(new() { PlayerId = pId, TrainingId = training.Id });
        }
    }

    public async Task<IEnumerable<Training>> GetAllTrainingsAsync()
    {
        var trainings = await _context.Trainings.ToListAsync();

        return trainings;
    }

    public async Task<IEnumerable<Training>> GetAllTrainingsAsync(TrainingsQueryStringParams trainingsFilter)
    {
        var trainings = trainingsFilter.Name == null
           ? _context.Trainings.Skip((trainingsFilter.PageNumber - 1) * trainingsFilter.PageSize).Take(trainingsFilter.PageSize)
           : _context.Trainings.Where(training =>
               (training.Name.ToLower() == trainingsFilter.Name!.ToLower() || string.IsNullOrEmpty(trainingsFilter.Name)))
               .Skip((trainingsFilter.PageNumber - 1) * trainingsFilter.PageSize).Take(trainingsFilter.PageSize);

        return await _sortHelper.ApplySort(trainings, trainingsFilter.OrderBy!).ToListAsync();
    }

    public async Task<int> GetAllTrainingsCountAsync()
    {
        return await _context.Trainings.CountAsync();
    }

    public async Task<Training?> GetTrainingByIdAsync(int trainingId)
    {
        var training = await _context.Trainings.Where(training => training.Id == trainingId).FirstOrDefaultAsync();

        return training;
    }

    public async Task<IEnumerable<Training>> GetTrainingsByCoachAsync(int coachId)
    {
        var trainings = await _context.Trainings.Where(training => training.CoachId == coachId).ToListAsync();

        return trainings;
    }

    public async Task<IEnumerable<Training>> GetTrainingsByPositionAsync(PositionGroup position)
    {
        var trainings = await (from training in _context.Trainings
                               join coach in _context.Coaches on training.CoachId equals coach.Id
                               where coach.Position == position
                               select training).ToListAsync();

        return trainings;
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

    public async Task UpdateTrainingAsync(Training training, IEnumerable<int> playerIDs)
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