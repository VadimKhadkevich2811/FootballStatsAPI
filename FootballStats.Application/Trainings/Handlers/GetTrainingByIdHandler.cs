using AutoMapper;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using FootballStats.Application.Trainings.Dtos;
using FootballStats.Application.Trainings.Queries.GetTrainingById;
using MediatR;

namespace FootballStats.Application.Trainings.Handlers;

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

        return trainingDto == null
            ? new Response<TrainingReadDto>(null, false, "Error during getting a coach by id",
                $"No Training Found By Id = {request.TrainingId}")
            : new Response<TrainingReadDto>(trainingDto);
    }
}