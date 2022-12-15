using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Mvc;

namespace FootballStats.Application.Coaches.Commands.CreateCoach;

public class CreateCoachCommandExceptionHandler : RequestExceptionHandler<CreateCoachCommand, Response<CoachReadDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public CreateCoachCommandExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(CreateCoachCommand request, Exception exception, RequestExceptionHandlerState<Response<CoachReadDto>> state)
    {
        string message = "Error during creating coach";
        state.SetHandled(new Response<CoachReadDto>(null, false, message, exception.Message));
        _logger.LogError(message);

    }
}