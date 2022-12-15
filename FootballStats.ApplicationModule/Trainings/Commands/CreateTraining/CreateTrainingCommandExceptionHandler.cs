using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Dtos.Trainings;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.ApplicationModule.Trainings.Commands.CreateTraining;

public class CreateTrainingCommandExceptionHandler : RequestExceptionHandler<CreateTrainingCommand, Response<TrainingReadDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public CreateTrainingCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(CreateTrainingCommand request, Exception exception, RequestExceptionHandlerState<Response<TrainingReadDto>> state)
    {
        string message = "Exception during creating training";
        state.SetHandled(new Response<TrainingReadDto>(null, false, new string[] { exception.Message }, message));
        _logger.LogError(message);

    }
}