using AutoMapper;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Trainings.Commands.DeleteTraining;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Handlers;

public class DeleteTrainingHandler : IRequestHandler<DeleteTrainingCommand, Response<bool>>
{
    private readonly ITrainingsRepository _repository;

    public DeleteTrainingHandler(ITrainingsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<bool>> Handle(DeleteTrainingCommand request, CancellationToken cancellationToken)
    {
        var training = await _repository.GetTrainingByIdAsync(request.TrainingId);

        if (training == null)
        {
            return new Response<bool>(false, false, null, $"No trainings found with ID = {request.TrainingId}");
        }

        _repository.RemoveTraining(training);

        return new Response<bool>(await _repository.SaveChangesAsync(), true);
    }
}