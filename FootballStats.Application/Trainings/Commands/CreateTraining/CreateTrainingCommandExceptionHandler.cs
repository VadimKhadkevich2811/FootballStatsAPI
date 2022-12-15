using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using FootballStats.Application.Trainings.Dtos;
using MediatR.Pipeline;

namespace FootballStats.Application.Trainings.Commands.CreateTraining;

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
        state.SetHandled(new Response<TrainingReadDto>(null, false, message, exception.Message));
        _logger.LogError(message);

    }
}