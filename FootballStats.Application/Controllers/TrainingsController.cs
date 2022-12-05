using FootballStats.ApplicationModule.Common.DTOs.Trainings;
using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.ApplicationModule.Common.Helpers;
using FootballStats.ApplicationModule.Common.Interfaces;
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
    public async Task<ActionResult> GetAllTrainingsAsync([FromQuery] TrainingsQueryStringParams filter)
    {
        var route = Request.Path.Value;
        var query = new GetAllTrainingsQuery(filter);
        var trainings = await _mediator.Send(query);
        var pagedReponse = PaginationHelper.CreatePagedReponse<TrainingReadDTO>(trainings.TrainingsList, filter,
            trainings.TrainingsTotalCount, _uriService, route);

        return Ok(pagedReponse);
    }

    //GET api/trainings/{trainingId}
    [HttpGet("{trainingId}", Name = "GetTrainingByIdAsync")]
    public async Task<ActionResult> GetTrainingByIdAsync(int trainingId)
    {
        var query = new GetTrainingByIdQuery(trainingId);
        var training = await _mediator.Send(query);

        return training != null
            ? Ok(new Response<TrainingReadDTO>(training, true))
            : NotFound(new Response<TrainingReadDTO>(null, true, null, $"No Training Found By Id = {trainingId}"));
    }

    //POST api/trainings
    [HttpPost]
    public async Task<ActionResult> CreateTrainingAsync(CreateTrainingCommand command)
    {
        var training = await _mediator.Send(command);

        return training == null
            ? BadRequest(new Response<TrainingReadDTO>(null, false, new[] { "Errors during new training creation." }))
            : CreatedAtRoute(nameof(GetTrainingByIdAsync), new { TrainingId = training.Id },
                new Response<TrainingReadDTO>(training, true, null, "Training is successfully created"));
    }

    //DELETE api/trainings/{trainingId}
    [HttpDelete("{trainingId}")]
    public async Task<ActionResult> DeleteTrainingAsync(int trainingId)
    {
        var result = await _mediator.Send(new DeleteTrainingCommand(trainingId));

        return result
            ? NoContent()
            : BadRequest(new Response<TrainingReadDTO>(null, false, new[] { "Errors during removing a training." }));
    }

    //PUT api/trainings/{trainingId}
    [HttpPut("{trainingId}")]
    public async Task<ActionResult> UpdateTrainingAsync(int trainingId, UpdateTrainingCommand command)
    {
        command.TrainingId = trainingId;

        var result = await _mediator.Send(command);

        return result
            ? NoContent()
            : BadRequest(new Response<TrainingReadDTO>(null, false, new[] { "Errors during updating a training." }));
    }

    //PATCH api/trainings/{trainingId}
    [HttpPatch("{trainingId}")]
    public async Task<ActionResult> UpdateTrainingDetailAsync(int trainingId, UpdateTrainingDetailCommand command)
    {
        command.TrainingId = trainingId;

        var result = await _mediator.Send(command);

        return result
            ? NoContent()
            : BadRequest(new Response<TrainingReadDTO>(null, false, new[] { "Errors during updating a training." }));
    }
}