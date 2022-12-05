using FootballStats.ApplicationModule.Common.DTOs;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Login.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballStats.Application.Controllers;

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
        var result = await _mediator.Send(command);
        if (result == null)
        {
            return NotFound(new Response<LoginDTO>(null, false, new[] { "Incorrect username or password" }));
        }
        return Ok(new Response<LoginDTO>(result, true));
    }
}