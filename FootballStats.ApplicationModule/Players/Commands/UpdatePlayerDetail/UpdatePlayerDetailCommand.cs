using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace FootballStats.ApplicationModule.Players.Commands.UpdatePlayerDetail;

public class UpdatePlayerDetailCommand : IRequest<Response<bool>>
{
    public int PlayerId { get; set; }
    public JsonPatchDocument<UpdatePlayerCommand> Item { get; set; } = default!;
}