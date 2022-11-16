using AutoMapper;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Trainings.Commands.DeleteTraining;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Handlers;

public class DeleteTrainingHandler : IRequestHandler<DeleteTrainingCommand, bool>
{
    private readonly ITrainingsRepository _repository;
    private readonly IMapper _mapper;

    public DeleteTrainingHandler(ITrainingsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteTrainingCommand request, CancellationToken cancellationToken)
    {
        var training = await _repository.GetTrainingById(request.TrainingId);

        if (training == null)
        {
            return false;
        }

        _repository.RemoveTraining(training);
        return await _repository.SaveChangesAsync();
    }
}