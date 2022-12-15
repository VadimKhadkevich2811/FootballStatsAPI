using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.SignUp.Commands;
using FootballStats.Application.SignUp.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballStats.API.Controllers;

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
            return Conflict(new ResponseProblemDetails(resultResponse.ErrorTitle, resultResponse.ErrorMessage,
                409, Request.Path.Value));
        }

        return Created("", resultResponse);
    }
}