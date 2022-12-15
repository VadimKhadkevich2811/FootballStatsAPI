using AutoMapper;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Trainings.Commands.UpdateTraining;
using MediatR;

namespace FootballStats.Application.Common.Trainings.Handlers;

public class UpdateTrainingHandler : IRequestHandler<UpdateTrainingCommand, Response<bool>>
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

    public async Task<Response<bool>> Handle(UpdateTrainingCommand request, CancellationToken cancellationToken)
    {
        var coach = await _coachesRepository.GetCoachByIdAsync(request.CoachId);

        if (coach == null)
        {
            return new Response<bool>(false, false, "Error during updating a training",
                $"No coaches found with ID = {request.CoachId}");
        }

        bool arePlayersValid = await _playersRepository.ArePlayersOfValidPositionAsync(coach.Position, request.PlayerIds);

        if (!arePlayersValid)
        {
            return new Response<bool>(false, false, "Error during updating a training",
                $"Players are not valid for a training with specified coach (coachID = {request.CoachId})");
        }

        var training = await _trainingsRepository.GetTrainingByIdAsync(request.TrainingId);

        if (training == null)
        {
            return new Response<bool>(false, false, "Error during updating a training",
                $"No trainings found with ID = {request.TrainingId}");
        }

        _mapper.Map(request, training);

        await _trainingsRepository.UpdateTrainingAsync(training, request.PlayerIds);

        return new Response<bool>(await _trainingsRepository.SaveChangesAsync(), true);
    }
}