using AutoMapper;
using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Dtos.Trainings;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Trainings.Queries.GetAllTrainings;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Handlers;

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
        trainingDtos.ForEach(async tdto => tdto.TrainedPlayers = _mapper.Map<List<PlayerReadDto>>(await _playersRepository.GetPlayersByTrainingId(tdto.Id)));
        var trainingsResult = new TrainingsListWithCountDto(trainingDtos, trainingsCount);
        var trainingsResponse = new Response<TrainingsListWithCountDto>(trainingsResult, true);

        return trainingsResponse;
    }
}