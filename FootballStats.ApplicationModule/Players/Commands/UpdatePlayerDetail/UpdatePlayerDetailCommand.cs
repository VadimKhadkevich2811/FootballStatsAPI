using FootballStats.ApplicationModule.Players.Commands.UpdatePlayer;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace FootballStats.ApplicationModule.Players.Commands.UpdatePlayerDetail;

public class UpdatePlayerDetailCommand : IRequest<bool>
{
    public int Id { get; set; }
    public JsonPatchDocument<UpdatePlayerCommand>? Item { get; set; }
}