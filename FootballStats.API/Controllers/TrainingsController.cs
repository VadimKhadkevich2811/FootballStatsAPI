using FootballStats.Application.Common.Helpers;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.QueryParams;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Trainings;
using FootballStats.Application.Trainings.Commands.CreateTraining;
using FootballStats.Application.Trainings.Commands.DeleteTraining;
using FootballStats.Application.Trainings.Commands.UpdateTraining;
using FootballStats.Application.Trainings.Commands.UpdateTrainingDetail;
using FootballStats.Application.Trainings.Dtos;
using FootballStats.Application.Trainings.Queries.GetAllTrainings;
using FootballStats.Application.Trainings.Queries.GetTrainingById;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FootballStats.API.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "auth")]
[Route("api/trainings")]
public class TrainingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUriService _uriService;
    private readonly IResponseWrapper<TrainingReadDto, TrainingsListWithCountDto> _responseWrapper;

    public TrainingsController(IMediator mediator, IUriService uriService,
        IResponseWrapper<TrainingReadDto, TrainingsListWithCountDto> responseWrapper)
    {
        _mediator = mediator;
        _uriService = uriService;
        _responseWrapper = responseWrapper;
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
            return BadRequest(new ResponseProblemDetails(trainingsResponse.ErrorTitle, trainingsResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        _responseWrapper.GenerateLinksForMany(trainingsResponse);

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
            return BadRequest(new ResponseProblemDetails(trainingResponse.ErrorTitle, trainingResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        if (trainingResponse.Data == null)
        {
            return NotFound(new ResponseProblemDetails(trainingResponse.ErrorTitle, trainingResponse.ErrorMessage,
                401, Request.Path.Value));
        }

        _responseWrapper.GenerateLinksForOne(trainingResponse);

        return Ok(trainingResponse);
    }

    //POST api/trainings
    [HttpPost]
    public async Task<ActionResult> CreateTrainingAsync(CreateTrainingCommand command)
    {
        var trainingResponse = await _mediator.Send(command);

        if (!trainingResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(trainingResponse.ErrorTitle, trainingResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        return CreatedAtRoute(nameof(GetTrainingByIdAsync), new { TrainingId = trainingResponse.Data!.Id }, trainingResponse);
    }

    //DELETE api/trainings/{trainingId}
    [HttpDelete("{trainingId}")]
    public async Task<ActionResult> DeleteTrainingAsync(int trainingId)
    {
        var resultResponse = await _mediator.Send(new DeleteTrainingCommand(trainingId));

        if (!resultResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(resultResponse.ErrorTitle, resultResponse.ErrorMessage,
                400, Request.Path.Value));
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
            return BadRequest(new ResponseProblemDetails(resultResponse.ErrorTitle, resultResponse.ErrorMessage,
                400, Request.Path.Value));
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
            return BadRequest(new ResponseProblemDetails(resultResponse.ErrorTitle, resultResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        return Ok(resultResponse);
    }
}