using FootballStats.Application.Coaches;
using FootballStats.Application.Coaches.Commands.CreateCoach;
using FootballStats.Application.Coaches.Commands.DeleteCoach;
using FootballStats.Application.Coaches.Commands.UpdateCoach;
using FootballStats.Application.Coaches.Commands.UpdateCoachDetail;
using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Coaches.Queries.GetAllCoaches;
using FootballStats.Application.Coaches.Queries.GetCoachById;
using FootballStats.Application.Coaches.Queries.GetFreeCoaches;
using FootballStats.Application.Common.Helpers;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.QueryParams;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Common.Wrappers.Links;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FootballStats.API.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "auth")]
[Route("api/coaches")]
public class CoachesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUriService _uriService;
    private readonly IResponseWrapper<CoachReadDto, CoachesListWithCountDto> _responseWrapper;

    public CoachesController(IMediator mediator, IUriService uriService,
        IResponseWrapper<CoachReadDto, CoachesListWithCountDto> responseWrapper)
    {
        _mediator = mediator;
        _uriService = uriService;
        _responseWrapper = responseWrapper;
    }

    //GET api/coaches
    [HttpGet]
    public async Task<ActionResult> GetAllCoachesAsync([FromQuery] CoachesQueryStringParams filter)
    {
        string? route = Request.Path.Value;
        var query = new GetAllCoachesQuery(filter);
        var coachesResponse = await _mediator.Send(query);

        if (!coachesResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(coachesResponse.ErrorTitle, coachesResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        _responseWrapper.GenerateLinksForMany(coachesResponse);

        var pagedReponse = PaginationHelper.CreatePagedReponse(coachesResponse.Data!.CoachesList,
            filter, coachesResponse.Data!.CoachesTotalCount, _uriService, route);

        return Ok(pagedReponse);
    }

    //GET api/coaches/{coachId}
    [HttpGet("{coachId}", Name = "GetCoachByIdAsync")]
    public async Task<ActionResult> GetCoachByIdAsync(int coachId)
    {
        var query = new GetCoachByIdQuery(coachId);
        var coachResponse = await _mediator.Send(query);

        if (!coachResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(coachResponse.ErrorTitle, coachResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        if (coachResponse.Data == null)
        {
            return NotFound(new ResponseProblemDetails(coachResponse.ErrorTitle, coachResponse.ErrorMessage,
                401, Request.Path.Value));
        }

        _responseWrapper.GenerateLinksForOne(coachResponse);

        return Ok(coachResponse);
    }

    //POST api/coaches
    [HttpPost]
    public async Task<ActionResult> CreateCoachAsync(CreateCoachCommand command)
    {
        var coachResponse = await _mediator.Send(command);

        if (!coachResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(coachResponse.ErrorTitle, coachResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        return CreatedAtRoute(nameof(GetCoachByIdAsync), new { CoachId = coachResponse.Data!.Id }, coachResponse);
    }

    //DELETE api/coaches/{coachId}
    [HttpDelete("{coachId}")]
    public async Task<ActionResult> DeleteCoachAsync(int coachId)
    {
        var resultResponse = await _mediator.Send(new DeleteCoachCommand(coachId));

        if (!resultResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(resultResponse.ErrorTitle, resultResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        return Ok(resultResponse);
    }

    //PUT api/coaches/{coachId}
    [HttpPut("{coachId}")]
    public async Task<ActionResult> UpdateCoachAsync(int coachId, UpdateCoachCommand command)
    {
        command.CoachId = coachId;

        var resultResponse = await _mediator.Send(command);

        if (!resultResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(resultResponse.ErrorTitle, resultResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        return Ok(resultResponse);
    }

    //PATCH api/coaches/{coachId}
    [HttpPatch("{coachId}")]
    public async Task<ActionResult> UpdateCoachDetailAsync(int coachId, UpdateCoachDetailCommand command)
    {
        command.CoachId = coachId;

        var resultResponse = await _mediator.Send(command);

        if (!resultResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(resultResponse.ErrorTitle, resultResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        return Ok(resultResponse);
    }

    [HttpGet]
    [Route("free")]
    public async Task<ActionResult> GetFreeCoachesByDateAsync([FromQuery] DateTime date)
    {
        var route = Request.Path.Value;
        var query = new GetFreeCoachesQuery(date);
        var freeCoachesResponse = await _mediator.Send(query);

        if (!freeCoachesResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(freeCoachesResponse.ErrorTitle, freeCoachesResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        _responseWrapper.GenerateLinksForMany(freeCoachesResponse);

        var pagedReponse = PaginationHelper.CreatePagedReponse<CoachReadDto>(freeCoachesResponse.Data!.CoachesList,
            new CoachesQueryStringParams(), freeCoachesResponse.Data!.CoachesTotalCount, _uriService, route);

        return Ok(pagedReponse);
    }
}