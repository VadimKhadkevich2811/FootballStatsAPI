using FootballStats.ApplicationModule.Common.Dtos;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.SignUp.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballStats.Application.Controllers;

[ApiController]
[Route("api/signup")]
public class SignUpController : ControllerBase
{
    private readonly IMediator _mediator;
    public SignUpController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //POST api/signup/
    [HttpPost]
    public async Task<ActionResult> SignUpAsync(SignUpCommand command)
    {
        var resultResponse = await _mediator.Send(command);

        if (!resultResponse.Succeeded)
        {
            return Conflict(resultResponse);
        }

        return Created("", resultResponse);
    }
}