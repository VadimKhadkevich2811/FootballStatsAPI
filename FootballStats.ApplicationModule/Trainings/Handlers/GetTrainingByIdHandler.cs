using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.DTOs.Trainings;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Trainings.Queries.GetTrainingById;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Handlers;

public class GetTrainingByIdHandler : IRequestHandler<GetTrainingByIdQuery, TrainingReadDTO>
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

    public async Task<TrainingReadDTO> Handle(GetTrainingByIdQuery request, CancellationToken cancellationToken)
    {
        var training = await _repository.GetTrainingByIdAsync(request.TrainingId);
        var trainingDTO = _mapper.Map<TrainingReadDTO>(training);
        trainingDTO.TrainedPlayers = _mapper.Map<List<PlayerReadDTO>>(await _playersRepository.GetPlayersByTrainingId(trainingDTO.Id));

        return trainingDTO;
    }
}