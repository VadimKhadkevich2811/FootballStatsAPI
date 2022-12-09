using AutoMapper;
using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Dtos.Trainings;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Trainings.Queries.GetTrainingById;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Handlers;

public class GetTrainingByIdHandler : IRequestHandler<GetTrainingByIdQuery, Response<TrainingReadDto>>
{
    private readonly ITrainingsRepository _repository;
    private readonly IPlayersRepository _playersRepository;
    private readonly IMapper _mapper;

    public GetTrainingByIdHandler(ITrainingsRepository repository, IPlayersRepository playersRepository, IMapper mapper)
    {
        _repository = repository;
        _playersRepository = playersRepository;
        _mapper = mapper;
    }

    public async Task<Response<TrainingReadDto>> Handle(GetTrainingByIdQuery request, CancellationToken cancellationToken)
    {
        var training = await _repository.GetTrainingByIdAsync(request.TrainingId);
        var trainingDto = _mapper.Map<TrainingReadDto>(training);
        trainingDto!.TrainedPlayers = _mapper.Map<List<PlayerReadDto>>(await _playersRepository.GetPlayersByTrainingId(trainingDto.Id));

        return new Response<TrainingReadDto>(trainingDto, true, null, trainingDto == null
            ? $"No Training Found By Id = {request.TrainingId}"
            : null);
    }
}