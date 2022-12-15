using AutoMapper;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using FootballStats.Application.Trainings.Dtos;
using FootballStats.Application.Trainings.Queries.GetAllTrainings;
using MediatR;

namespace FootballStats.Application.Trainings.Handlers;

public class GetAllTrainingsHandler : IRequestHandler<GetAllTrainingsQuery, Response<TrainingsListWithCountDto>>
{
    private readonly ITrainingsRepository _repository;
    private readonly IPlayersRepository _playersRepository;
    private readonly IMapper _mapper;

    public GetAllTrainingsHandler(ITrainingsRepository repository, IPlayersRepository playersRepository, IMapper mapper)
    {
        _repository = repository;
        _playersRepository = playersRepository;
        _mapper = mapper;
    }

    public async Task<Response<TrainingsListWithCountDto>> Handle(GetAllTrainingsQuery request, CancellationToken cancellationToken)
    {
        var filter = request.TrainingsQueryStringParams;
        var trainings = await _repository.GetAllTrainingsAsync(filter);
        var trainingsCount = await _repository.GetAllTrainingsCountAsync();
        var trainingDtos = _mapper.Map<List<TrainingReadDto>>(trainings);
        foreach (var trainingDTO in trainingDtos)
        {
            var trainedPlayers = await _playersRepository.GetPlayersByTrainingId(trainingDTO.Id);
            trainingDTO.TrainedPlayers = _mapper.Map<List<PlayerReadDto>>(trainedPlayers);
        }
        var trainingsResult = new TrainingsListWithCountDto(trainingDtos, trainingsCount);
        var trainingsResponse = new Response<TrainingsListWithCountDto>(trainingsResult, true);

        return trainingsResponse;
    }
}