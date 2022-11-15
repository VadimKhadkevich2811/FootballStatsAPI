using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Trainings;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Trainings.Queries.GetAllTrainingsQuery;
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
        var paginationFilter = request.PaginationFilter;
        var trainings = await _repository.GetAllTrainings(paginationFilter.PageNumber, paginationFilter.PageSize);
        var trainingsCount = await _repository.GetAllTrainingsCount();
        var trainingDTOs = _mapper.Map<List<TrainingReadDTO>>(trainings);

        return new TrainingsListWithCountDTO(trainingDTOs, trainingsCount);
    }
}