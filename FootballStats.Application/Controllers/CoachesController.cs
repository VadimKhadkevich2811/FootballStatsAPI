using FootballStats.ApplicationModule.Coaches.Commands.CreateCoach;
using FootballStats.ApplicationModule.Coaches.Commands.DeleteCoach;
using FootballStats.ApplicationModule.Coaches.Commands.UpdateCoach;
using FootballStats.ApplicationModule.Coaches.Commands.UpdateCoachDetail;
using FootballStats.ApplicationModule.Common.DTOs.Coaches;
using FootballStats.ApplicationModule.Common.Filters;
using FootballStats.ApplicationModule.Common.Helpers;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Trainings.Queries.GetAllCoachesQuery;
using FootballStats.ApplicationModule.Trainings.Queries.GetCoachById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballStats.Application.Controllers;

[ApiController]
//[Authorize(AuthenticationSchemes = "Bearer")]
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
    public async Task<ActionResult> GetAllCoachesAsync([FromQuery] PaginationFilter filter)
    {
        string? route = Request.Path.Value;
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var query = new GetAllCoachesQuery(validFilter);
        var coaches = await _mediator.Send(query);
        var pagedReponse = PaginationHelper.CreatePagedReponse<CoachReadDTO>(coaches.CoachesList,
            validFilter, coaches.CoachesTotalCount, _uriService, route);
        return Ok(pagedReponse);
    }

    //GET api/coaches/{coachId}
    [HttpGet("{coachId}", Name = "GetCoachByIdAsync")]
    public async Task<ActionResult> GetCoachByIdAsync(int coachId)
    {
        var query = new GetCoachByIdQuery(coachId);
        var coach = await _mediator.Send(query);

        return coach != null ? Ok(new Response<CoachReadDTO>(coach, true)) : NotFound();
    }

    //POST api/coaches
    [HttpPost]
    public async Task<ActionResult> CreateCoachAsync(CreateCoachCommand command)
    {
        var result = await _mediator.Send(command);

        return result == null ? BadRequest("Errors during new coach creation.")
            : CreatedAtRoute(nameof(GetCoachByIdAsync), new { CoachId = result.Id }, result);
    }

    //DELETE api/coaches/{coachId}
    [HttpDelete("{coachId}")]
    public async Task<ActionResult> DeleteCoachAsync(int coachId)
    {
        var result = await _mediator.Send(new DeleteCoachCommand(coachId));

        return result ? NoContent() : BadRequest("Errors during removing a coach.");
    }

    //PUT api/coaches/{coachId}
    [HttpPut("{coachId}")]
    public async Task<ActionResult> UpdateCoachAsync(int coachId, UpdateCoachCommand command)
    {
        command.CoachId = coachId;

        var result = await _mediator.Send(command);

        return result ? NoContent() : BadRequest("Errors during updating a coach.");
    }

    //PATCH api/coaches/{coachId}
    [HttpPatch("{coachId}")]
    public async Task<ActionResult> UpdateCoachDetailAsync(int coachId, UpdateCoachDetailCommand command)
    {
        command.CoachId = coachId;

        var result = await _mediator.Send(command);

        return result ? NoContent() : BadRequest("Errors during updating a coach.");
    }
}