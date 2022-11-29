using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.QueryParams;
using FootballStats.ApplicationModule.Common.Helpers;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Players.Commands.CreatePlayer;
using FootballStats.ApplicationModule.Players.Commands.DeletePlayer;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayerDetail;
using FootballStats.ApplicationModule.Players.Queries.GetPlayerById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FootballStats.ApplicationModule.Players.Queries.GetFreePlayers;
using FootballStats.ApplicationModule.Players.Queries.GetAllPlayers;

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
        var players = await _mediator.Send(query);
        var pagedReponse = PaginationHelper.CreatePagedReponse<PlayerReadDTO>(players.PlayersList, filter, players.PlayersTotalCount, _uriService, route);
        return Ok(pagedReponse);
    }

    //GET api/players/{playerId}
    [HttpGet("{playerId}", Name = "GetPlayerByIdAsync")]
    public async Task<ActionResult> GetPlayerByIdAsync(int playerId)
    {
        var query = new GetPlayerByIdQuery(playerId);
        var player = await _mediator.Send(query);

        return player != null ? Ok(new Response<PlayerReadDTO>(player, true)) : NotFound();
    }

    //POST api/players
    [HttpPost]
    public async Task<ActionResult> CreatePlayerAsync(CreatePlayerCommand command)
    {
        var result = await _mediator.Send(command);

        return result == null ? BadRequest("Errors during new player creation.")
            : CreatedAtRoute(nameof(GetPlayerByIdAsync), new { PlayerId = result.Id }, result);
    }

    //DELETE api/players/{playerId}
    [HttpDelete("{playerId}")]
    public async Task<ActionResult> DeletePlayerAsync(int playerId)
    {
        var result = await _mediator.Send(new DeletePlayerCommand(playerId));

        return result ? NoContent() : BadRequest("Errors during removing a player.");
    }

    //PUT api/players/{playerId}
    [HttpPut("{playerId}")]
    public async Task<ActionResult> UpdatePlayerAsync(int playerId, UpdatePlayerCommand command)
    {
        command.PlayerId = playerId;

        var result = await _mediator.Send(command);

        return result ? NoContent() : BadRequest("Errors during updating a player.");
    }

    //PATCH api/players/{playerId}
    [HttpPatch("{playerId}")]
    public async Task<ActionResult> UpdatePlayerDetailAsync(int playerId, UpdatePlayerDetailCommand command)
    {
        command.PlayerId = playerId;

        var result = await _mediator.Send(command);

        return result ? NoContent() : BadRequest("Errors during updating a player.");
    }

    //GET api/players/freeplayers
    [HttpGet]
    [Route("freeplayers")]
    public async Task<ActionResult> GetFreePlayersAsync([FromQuery] PlayersQueryStringParams filter)
    {
        var route = Request.Path.Value;
        var query = new GetFreePlayersQuery(filter);
        var freePlayers = await _mediator.Send(query);
        var pagedReponse = PaginationHelper.CreatePagedReponse<PlayerReadDTO>(freePlayers.PlayersList, filter, freePlayers.PlayersTotalCount, _uriService, route);
        return Ok(pagedReponse);
    }
}