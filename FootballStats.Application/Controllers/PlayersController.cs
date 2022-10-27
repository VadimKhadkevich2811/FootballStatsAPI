using FootballStats.ApplicationModule.Players.Commands.CreatePlayer;
using FootballStats.ApplicationModule.Players.Commands.DeletePlayer;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayerDetail;
using FootballStats.ApplicationModule.Players.Queries.GetAllPlayersQuery;
using FootballStats.ApplicationModule.Players.Queries.GetPlayerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootballStats.Application.Controllers;

[ApiController]
[Route("api/players")]
public class PlayersController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlayersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //GET api/players
    [HttpGet]
    public async Task<ActionResult> GetAllPlayersAsync()
    {
        var query = new GetAllPlayersQuery();
        var players = await _mediator.Send(query);

        return Ok(players);
    }

    //GET api/players/{playerId}
    [HttpGet("{playerId}", Name = "GetPlayerByIdAsync")]
    public async Task<ActionResult> GetPlayerByIdAsync(int playerId)
    {
        var query = new GetPlayerByIdQuery(playerId);
        var player = await _mediator.Send(query);

        return player != null ? Ok(player) : NotFound();
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

        return result ? NoContent() : BadRequest("Errors during removing a user.");
    }

    //PUT api/players/{playerId}
    [HttpPut("{playerId}")]
    public async Task<ActionResult> UpdatePlayerAsync(int playerId, UpdatePlayerCommand command)
    {
        command.PlayerId = playerId;

        var result = await _mediator.Send(command);

        return result ? NoContent() : BadRequest("Errors during updating a user.");
    }

    //PATCH api/players/{playerId}
    [HttpPatch("{playerId}")]
    public async Task<ActionResult> UpdatePlayerDetailAsync(int playerId, UpdatePlayerDetailCommand command)
    {
        command.PlayerId = playerId;

        var result = await _mediator.Send(command);

        return result ? NoContent() : BadRequest("Errors during updating a user.");
    }
}