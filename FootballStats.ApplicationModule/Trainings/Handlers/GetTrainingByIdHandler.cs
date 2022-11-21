using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Trainings;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Trainings.Queries.GetTrainingById;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Handlers;

public class GetTrainingByIdHandler : IRequestHandler<GetTrainingByIdQuery, TrainingReadDTO>
{
    private readonly ITrainingsRepository _repository;
    private readonly IMapper _mapper;

    public GetTrainingByIdHandler(ITrainingsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TrainingReadDTO> Handle(GetTrainingByIdQuery request, CancellationToken cancellationToken)
    {
        var training = await _repository.GetTrainingByIdAsync(request.TrainingId);
        var trainingDTO = _mapper.Map<TrainingReadDTO>(training);

        return trainingDTO;
    }
}