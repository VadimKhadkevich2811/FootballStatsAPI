using FootballStats.ApplicationModule.Login.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballStats.Application.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private IMediator _mediator;
    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> LoginAsync(LoginCommand command)
    {
        var result = await _mediator.Send(command);
        if (result == null)
        {
            return NotFound("Incorrect username or password");
        }
        return Ok(result);
    }
}