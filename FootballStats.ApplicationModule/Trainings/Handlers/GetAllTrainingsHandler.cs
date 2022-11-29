using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Trainings;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Trainings.Queries.GetAllTrainings;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Handlers;

public class GetAllTrainingsHandler : IRequestHandler<GetAllTrainingsQuery, TrainingsListWithCountDTO>
{
    private readonly ITrainingsRepository _repository;
    private readonly IMapper _mapper;

    public GetAllTrainingsHandler(ITrainingsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TrainingsListWithCountDTO> Handle(GetAllTrainingsQuery request, CancellationToken cancellationToken)
    {
        var filter = request.TrainingsQueryStringParams;
        var trainings = await _repository.GetAllTrainingsAsync(filter.PageNumber, filter.PageSize);
        var trainingsCount = await _repository.GetAllTrainingsCountAsync();
        var trainingDTOs = _mapper.Map<List<TrainingReadDTO>>(trainings);

        return new TrainingsListWithCountDTO(trainingDTOs, trainingsCount);
    }
}