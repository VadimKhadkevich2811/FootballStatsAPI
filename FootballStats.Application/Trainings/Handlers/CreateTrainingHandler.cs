using AutoMapper;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Trainings.Commands.CreateTraining;
using FootballStats.Application.Trainings.Dtos;
using FootballStats.Domain.Entities;
using MediatR;

namespace FootballStats.Application.Common.Trainings.Handlers;

public class CreateTrainingHandler : IRequestHandler<CreateTrainingCommand, Response<TrainingReadDto>>
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

    public async Task<Response<TrainingReadDto>> Handle(CreateTrainingCommand request, CancellationToken cancellationToken)
    {
        var training = _mapper.Map<Training>(request);

        var coach = await _coachesRepository.GetCoachByIdAsync(training.CoachId);

        if (coach == null)
        {
            return new Response<TrainingReadDto>(null, false, "Error during creating a training",
                $"No coaches found with ID = {training.CoachId}");
        }

        bool arePlayersValid = await _playersRepository.ArePlayersOfValidPositionAsync(coach.Position, request.PlayerIDs);

        if (!arePlayersValid)
        {
            return new Response<TrainingReadDto>(null, false, "Error during creating a training",
                $"Players are not valid for a training with specified coach (coachID = {training.CoachId})");
        }

        await _trainingsRepository.AddTrainingAsync(training, request.PlayerIDs);

        await _trainingsRepository.SaveChangesAsync();

        var newTraining = _mapper.Map<TrainingReadDto>(training);

        var newTrainingResponse = new Response<TrainingReadDto>(newTraining, true);

        return newTrainingResponse;
    }
}