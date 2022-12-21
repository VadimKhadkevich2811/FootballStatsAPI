using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using MediatR.Pipeline;

namespace FootballStats.Application.Coaches.Queries.GetCoachById;

public class GetCoachByIdQueryExceptionHandler : RequestExceptionHandler<GetCoachByIdQuery, Response<CoachReadDto>, Exception>
{
    private readonly ILoggerManager _logger;

    public GetCoachByIdQueryExceptionHandler(ILoggerManager logger)
    {
        _logger = logger;
    }

    protected override void Handle(GetCoachByIdQuery request, Exception exception, RequestExceptionHandlerState<Response<CoachReadDto>> state)
    {
        string message = "Exception during getting coach by Id";
        state.SetHandled(new Response<CoachReadDto>(null, false, message, exception.Message));
        _logger.LogError(message);

    }
}