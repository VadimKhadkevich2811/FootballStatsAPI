using AutoMapper;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Trainings.Commands.UpdateTraining;
using FootballStats.ApplicationModule.Trainings.Commands.UpdateTrainingDetail;
using MediatR;

namespace FootballStats.ApplicationModule.Common.Trainings.Handlers;

public class UpdateTrainingDetailHandler : IRequestHandler<UpdateTrainingDetailCommand, Response<bool>>
{
    private readonly ITrainingsRepository _repository;
    private readonly IMapper _mapper;
    public UpdateTrainingDetailHandler(ITrainingsRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(UpdateTrainingDetailCommand request, CancellationToken cancellationToken)
    {
        var training = await _repository.GetTrainingByIdAsync(request.TrainingId);

        if (training == null)
        {
            return new Response<bool>(false, false, null, $"No trainings found with ID = {request.TrainingId}");
        }
        var trainingToPatch = _mapper.Map<UpdateTrainingCommand>(training);
        request.Item.ApplyTo(trainingToPatch);

        _mapper.Map(trainingToPatch, training);

        await _repository.UpdateTrainingAsync(training, trainingToPatch.PlayerIds);

        return new Response<bool>(await _repository.SaveChangesAsync(), true);
    }
}