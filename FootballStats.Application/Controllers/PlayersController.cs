using FootballStats.ApplicationModule.Common.Dtos.Players;
using FootballStats.ApplicationModule.Common.Helpers;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Players.Commands.CreatePlayer;
using FootballStats.ApplicationModule.Players.Commands.DeletePlayer;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayerDetail;
using FootballStats.ApplicationModule.Players.Queries.GetAllPlayers;
using FootballStats.ApplicationModule.Players.Queries.GetFreePlayers;
using FootballStats.ApplicationModule.Players.Queries.GetPlayerById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballStats.Application.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/players")]
public class PlayersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUriService _uriService;

    public PlayersController(IMediator mediator, IUriService uriService)
    {
        _mediator = mediator;
        _uriService = uriService;
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
            return BadRequest(playersResponse);
        }

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
            return BadRequest(playerResponse);
        }

        return playerResponse.Data != null
            ? Ok(playerResponse)
            : NotFound(playerResponse);
    }

    //POST api/players
    [HttpPost]
    public async Task<ActionResult> CreatePlayerAsync(CreatePlayerCommand command)
    {
        var playerResponse = await _mediator.Send(command);

        if (!playerResponse.Succeeded)
        {
            return BadRequest(playerResponse);
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
            return BadRequest(resultResponse);
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
            return BadRequest(resultResponse);
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
            return BadRequest(resultResponse);
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
            return BadRequest(freePlayersResponse);
        }

        var pagedReponse = PaginationHelper.CreatePagedReponse<PlayerReadDto>(freePlayersResponse.Data!.PlayersList,
            new CoachesQueryStringParams(), freePlayersResponse.Data!.PlayersTotalCount, _uriService, route);

        return Ok(pagedReponse);
    }
}