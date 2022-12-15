using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Commands.UpdatePlayer;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace FootballStats.Application.Players.Commands.UpdatePlayerDetail;

public class UpdatePlayerDetailCommand : IRequest<Response<bool>>
{
    public int PlayerId { get; set; }
    public JsonPatchDocument<UpdatePlayerCommand> Item { get; set; } = default!;
}