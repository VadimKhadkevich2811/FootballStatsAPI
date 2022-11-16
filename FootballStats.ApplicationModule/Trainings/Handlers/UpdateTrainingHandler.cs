using AutoMapper;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Trainings.Commands.UpdateTraining;
using MediatR;

namespace FootballStats.ApplicationModule.Common.Trainings.Handlers;

public class UpdateTrainingHandler : IRequestHandler<UpdateTrainingCommand, bool>
{
    private readonly ITrainingsRepository _trainingsRepository;
    private readonly ICoachesRepository _coachesRepository;
    private readonly IPlayersRepository _playersRepository;
    private readonly IMapper _mapper;

    public UpdateTrainingHandler(ITrainingsRepository trainingsRepository,
        ICoachesRepository coachesRepository,
        IPlayersRepository playersRepository,
        IMapper mapper)
    {
        _trainingsRepository = trainingsRepository;
        _coachesRepository = coachesRepository;
        _playersRepository = playersRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateTrainingCommand request, CancellationToken cancellationToken)
    {
        var coach = await _coachesRepository.GetCoachById(request.CoachId);

        if (coach == null)
        {
            return false;
        }

        bool arePlayersValid = await _playersRepository.ArePlayersOfValidPosition(coach.Position);

        if (!arePlayersValid)
        {
            return false;
        }

        var training = await _trainingsRepository.GetTrainingById(request.TrainingId);

        if (training == null)
        {
            return false;
        }

        _mapper.Map(request, training);

        _trainingsRepository.UpdateTraining(training, request.PlayerIds);
        return await _trainingsRepository.SaveChangesAsync();
    }
}