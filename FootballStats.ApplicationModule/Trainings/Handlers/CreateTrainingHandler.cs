using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Trainings;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Trainings.Commands.CreateTraining;
using FootballStats.Domain.Entities;
using MediatR;

namespace FootballStats.ApplicationModule.Common.Trainings.Handlers;

public class CreateTrainingHandler : IRequestHandler<CreateTrainingCommand, TrainingReadDTO>
{
    private readonly ITrainingsRepository _trainingsRepository;
    private readonly ICoachesRepository _coachesRepository;
    private readonly IPlayersRepository _playersRepository;
    private readonly IMapper _mapper;
    public CreateTrainingHandler(ITrainingsRepository trainingsRepository,
        ICoachesRepository coachesRepository,
        IPlayersRepository playersRepository,
        IMapper mapper)
    {
        _trainingsRepository = trainingsRepository;
        _coachesRepository = coachesRepository;
        _playersRepository = playersRepository;
        _mapper = mapper;
    }

    public async Task<TrainingReadDTO> Handle(CreateTrainingCommand request, CancellationToken cancellationToken)
    {
        var training = _mapper.Map<Training>(request);

        var coach = await _coachesRepository.GetCoachById(training.CoachId);

        if (coach == null)
        {
            return null;
        }

        bool arePlayersValid = await _playersRepository.ArePlayersOfValidPosition(coach.Position);

        if (!arePlayersValid)
        {
            return null;
        }

        await _trainingsRepository.AddTraining(training, request.PlayerIDs);

        await _trainingsRepository.SaveChangesAsync();

        var newTraining = _mapper.Map<TrainingReadDTO>(training);

        return newTraining;
    }
}