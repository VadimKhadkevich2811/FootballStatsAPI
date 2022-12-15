using AutoMapper;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Trainings.Commands.UpdateTraining;
using FootballStats.Application.Trainings.Commands.UpdateTrainingDetail;
using MediatR;

namespace FootballStats.Application.Common.Trainings.Handlers;

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