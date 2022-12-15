using FootballStats.ApplicationModule.Coaches.Commands.CreateCoach;
using FootballStats.ApplicationModule.Coaches.Commands.DeleteCoach;
using FootballStats.ApplicationModule.Coaches.Commands.UpdateCoach;
using FootballStats.ApplicationModule.Coaches.Commands.UpdateCoachDetail;
using FootballStats.ApplicationModule.Coaches.Queries.GetAllCoaches;
using FootballStats.ApplicationModule.Coaches.Queries.GetCoachById;
using FootballStats.ApplicationModule.Coaches.Queries.GetFreeCoaches;
using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Helpers;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.QueryParams;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballStats.Application.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/coaches")]
public class CoachesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUriService _uriService;

    public CoachesController(IMediator mediator, IUriService uriService)
    {
        _mediator = mediator;
        _uriService = uriService;
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
            return BadRequest(coachesResponse);
        }

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
            return BadRequest(coachResponse);
        }

        return coachResponse.Data != null
            ? Ok(coachResponse)
            : NotFound(coachResponse);
    }

    //POST api/coaches
    [HttpPost]
    public async Task<ActionResult> CreateCoachAsync(CreateCoachCommand command)
    {
        var coachResponse = await _mediator.Send(command);

        if (!coachResponse.Succeeded)
        {
            return BadRequest(coachResponse);
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
            return BadRequest(resultResponse);
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
            return BadRequest(resultResponse);
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
            return BadRequest(resultResponse);
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
            return BadRequest(freeCoachesResponse);
        }

        var pagedReponse = PaginationHelper.CreatePagedReponse<CoachReadDto>(freeCoachesResponse.Data!.CoachesList,
            new CoachesQueryStringParams(), freeCoachesResponse.Data!.CoachesTotalCount, _uriService, route);

        return Ok(pagedReponse);
    }
}