using FootballStats.Application.Common.Helpers;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.QueryParams;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players;
using FootballStats.Application.Players.Commands.CreatePlayer;
using FootballStats.Application.Players.Commands.DeletePlayer;
using FootballStats.Application.Players.Commands.UpdatePlayer;
using FootballStats.Application.Players.Commands.UpdatePlayerDetail;
using FootballStats.Application.Players.Dtos;
using FootballStats.Application.Players.Queries.GetAllPlayers;
using FootballStats.Application.Players.Queries.GetFreePlayers;
using FootballStats.Application.Players.Queries.GetPlayerById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FootballStats.API.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/players")]
public class PlayersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUriService _uriService;
    private readonly LinkGenerator _linkGenerator;

    public PlayersController(IMediator mediator, IUriService uriService, LinkGenerator linkGenerator)
    {
        _mediator = mediator;
        _uriService = uriService;
        _linkGenerator = linkGenerator;
    }

    //GET api/players
    [HttpGet]
    public async Task<ActionResult> GetAllPlayersAsync([FromQuery] PlayersQueryStringParams filter)
    {
        var route = Request.Path.Value;
        var query = new GetAllPlayersQuery(filter);
        var playersResponse = await _mediator.Send(query);

        if (!playersResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(playersResponse.ErrorTitle, playersResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        PlayerResponseWrapper.GenerateLinksForPlayers(playersResponse, _linkGenerator, HttpContext);

        var pagedReponse = PaginationHelper.CreatePagedReponse(playersResponse.Data!.PlayersList,
            filter, playersResponse.Data!.PlayersTotalCount, _uriService, route);

        return Ok(pagedReponse);
    }

    //GET api/players/{playerId}
    [HttpGet("{playerId}", Name = "GetPlayerByIdAsync")]
    public async Task<ActionResult> GetPlayerByIdAsync(int playerId)
    {
        var query = new GetPlayerByIdQuery(playerId);
        var playerResponse = await _mediator.Send(query);

        if (!playerResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(playerResponse.ErrorTitle, playerResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        if (playerResponse.Data == null)
        {
            return NotFound(new ResponseProblemDetails(playerResponse.ErrorTitle, playerResponse.ErrorMessage,
                401, Request.Path.Value));
        }

        PlayerResponseWrapper.GenerateLinksForPlayer(playerResponse, _linkGenerator, HttpContext);

        return Ok(playerResponse);
    }

    //POST api/players
    [HttpPost]
    public async Task<ActionResult> CreatePlayerAsync(CreatePlayerCommand command)
    {
        var playerResponse = await _mediator.Send(command);

        if (!playerResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(playerResponse.ErrorTitle, playerResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        return CreatedAtRoute(nameof(GetPlayerByIdAsync), new { CoachId = playerResponse.Data!.Id }, playerResponse);
    }

    //DELETE api/players/{playerId}
    [HttpDelete("{playerId}")]
    public async Task<ActionResult> DeletePlayerAsync(int playerId)
    {
        var resultResponse = await _mediator.Send(new DeletePlayerCommand(playerId));

        if (!resultResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(resultResponse.ErrorTitle, resultResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        return Ok(resultResponse);
    }

    //PUT api/players/{playerId}
    [HttpPut("{playerId}")]
    public async Task<ActionResult> UpdatePlayerAsync(int playerId, UpdatePlayerCommand command)
    {
        command.PlayerId = playerId;

        var resultResponse = await _mediator.Send(command);

        if (!resultResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(resultResponse.ErrorTitle, resultResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        return Ok(resultResponse);
    }

    //PATCH api/players/{playerId}
    [HttpPatch("{playerId}")]
    public async Task<ActionResult> UpdatePlayerDetailAsync(int playerId, UpdatePlayerDetailCommand command)
    {
        command.PlayerId = playerId;

        var resultResponse = await _mediator.Send(command);

        if (!resultResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(resultResponse.ErrorTitle, resultResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        return Ok(resultResponse);
    }

    //GET api/players/free
    [HttpGet]
    [Route("free")]
    public async Task<ActionResult> GetFreePlayersByDateAsync([FromQuery] DateTime date)
    {
        var route = Request.Path.Value;
        var query = new GetFreePlayersQuery(date);
        var freePlayersResponse = await _mediator.Send(query);

        if (!freePlayersResponse.Succeeded)
        {
            return BadRequest(new ResponseProblemDetails(freePlayersResponse.ErrorTitle, freePlayersResponse.ErrorMessage,
                400, Request.Path.Value));
        }

        var pagedReponse = PaginationHelper.CreatePagedReponse<PlayerReadDto>(freePlayersResponse.Data!.PlayersList,
            new CoachesQueryStringParams(), freePlayersResponse.Data!.PlayersTotalCount, _uriService, route);

        return Ok(pagedReponse);
    }
}