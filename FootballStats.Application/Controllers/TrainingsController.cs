using FootballStats.ApplicationModule.Common.Dtos.Trainings;
using FootballStats.ApplicationModule.Common.Helpers;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Trainings.Commands.CreateTraining;
using FootballStats.ApplicationModule.Trainings.Commands.DeleteTraining;
using FootballStats.ApplicationModule.Trainings.Commands.UpdateTraining;
using FootballStats.ApplicationModule.Trainings.Commands.UpdateTrainingDetail;
using FootballStats.ApplicationModule.Trainings.Queries.GetAllTrainings;
using FootballStats.ApplicationModule.Trainings.Queries.GetTrainingById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballStats.Application.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
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
    public async Task<ActionResult> GetAllTrainingsAsync([FromQuery] TrainingsQueryStringParams filter)
    {
        var route = Request.Path.Value;
        var query = new GetAllTrainingsQuery(filter);
        var trainingsResponse = await _mediator.Send(query);

        if (!trainingsResponse.Succeeded)
        {
            return BadRequest(trainingsResponse);
        }

        var pagedReponse = PaginationHelper.CreatePagedReponse(trainingsResponse.Data!.TrainingsList,
            filter, trainingsResponse.Data!.TrainingsTotalCount, _uriService, route);

        return Ok(pagedReponse);
    }

    //GET api/trainings/{trainingId}
    [HttpGet("{trainingId}", Name = "GetTrainingByIdAsync")]
    public async Task<ActionResult> GetTrainingByIdAsync(int trainingId)
    {
        var query = new GetTrainingByIdQuery(trainingId);
        var trainingResponse = await _mediator.Send(query);

        if (!trainingResponse.Succeeded)
        {
            return BadRequest(trainingResponse);
        }

        return trainingResponse.Data != null
            ? Ok(trainingResponse)
            : NotFound(trainingResponse);
    }

    //POST api/trainings
    [HttpPost]
    public async Task<ActionResult> CreateTrainingAsync(CreateTrainingCommand command)
    {
        var trainingResponse = await _mediator.Send(command);

        if (!trainingResponse.Succeeded)
        {
            return BadRequest(trainingResponse);
        }

        return CreatedAtRoute(nameof(GetTrainingByIdAsync), new { CoachId = trainingResponse.Data!.Id }, trainingResponse);
    }

    //DELETE api/trainings/{trainingId}
    [HttpDelete("{trainingId}")]
    public async Task<ActionResult> DeleteTrainingAsync(int trainingId)
    {
        var resultResponse = await _mediator.Send(new DeleteTrainingCommand(trainingId));

        if (!resultResponse.Succeeded)
        {
            return BadRequest(resultResponse);
        }

        return Ok(resultResponse);
    }

    //PUT api/trainings/{trainingId}
    [HttpPut("{trainingId}")]
    public async Task<ActionResult> UpdateTrainingAsync(int trainingId, UpdateTrainingCommand command)
    {
        command.TrainingId = trainingId;

        var resultResponse = await _mediator.Send(command);

        if (!resultResponse.Succeeded)
        {
            return BadRequest(resultResponse);
        }

        return Ok(resultResponse);
    }

    //PATCH api/trainings/{trainingId}
    [HttpPatch("{trainingId}")]
    public async Task<ActionResult> UpdateTrainingDetailAsync(int trainingId, UpdateTrainingDetailCommand command)
    {
        command.TrainingId = trainingId;

        var resultResponse = await _mediator.Send(command);

        if (!resultResponse.Succeeded)
        {
            return BadRequest(resultResponse);
        }

        return Ok(resultResponse);
    }
}