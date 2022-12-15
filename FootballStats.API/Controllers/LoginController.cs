using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Login.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballStats.API.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly IMediator _mediator;
    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //POST api/login/
    [HttpPost]
    public async Task<ActionResult> LoginAsync(LoginCommand command)
    {
        var resultResponse = await _mediator.Send(command);
        if (!resultResponse.Succeeded && resultResponse.Data == null)
        {
            return NotFound(new ResponseProblemDetails(resultResponse.ErrorTitle, resultResponse.ErrorMessage,
                401, Request.Path.Value));
        }
        return Ok(resultResponse);
    }
}