using FootballStats.ApplicationModule.Common.DTOs.Trainings;
using FootballStats.ApplicationModule.Common.Filters;
using FootballStats.ApplicationModule.Common.Helpers;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Trainings.Queries.GetAllTrainingsQuery;
using FootballStats.ApplicationModule.Trainings.Queries.GetTrainingById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballStats.Application.Controllers;

[ApiController]
//[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/trainings")]
public class TrainingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUriService _uriService;

    public TrainingsController(IMediator mediator, IUriService uriService)
    {
        _mediator = mediator;
        _uriService = uriService;
    }

    //GET api/trainings
    [HttpGet]
    public async Task<ActionResult> GetAllTrainingsAsync([FromQuery] PaginationFilter filter)
    {
        var route = Request.Path.Value;
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var query = new GetAllTrainingsQuery(validFilter);
        var trainings = await _mediator.Send(query);
        var pagedReponse = PaginationHelper.CreatePagedReponse<TrainingReadDTO>(trainings.TrainingsList, 
            validFilter, trainings.TrainingsTotalCount, _uriService, route);
        return Ok(pagedReponse);
    }

    //GET api/trainings/{trainingId}
    [HttpGet("{trainingId}", Name = "GetTrainingByIdAsync")]
    public async Task<ActionResult> GetTrainingByIdAsync(int trainingId)
    {
        var query = new GetTrainingByIdQuery(trainingId);
        var training = await _mediator.Send(query);

        return training != null ? Ok(new Response<TrainingReadDTO>(training, true)) : NotFound();
    }
}