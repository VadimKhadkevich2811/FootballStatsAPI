using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using MediatR.Pipeline;

namespace FootballStats.Application.Players.Queries.GetPlayerById;

public class GetPlayerByIdQueryExceptionHandler : RequestExceptionHandler<GetPlayerByIdQuery, Response<PlayerReadDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public GetPlayerByIdQueryExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(GetPlayerByIdQuery request, Exception exception, RequestExceptionHandlerState<Response<PlayerReadDto>> state)
    {
        string message = "Error during getting player by Id";
        state.SetHandled(new Response<PlayerReadDto>(null, false, message, exception.Message));
        _logger.LogError(message);

    }
}