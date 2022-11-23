using FootballStats.ApplicationModule.Coaches.Commands.UpdateCoach;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace FootballStats.ApplicationModule.Coaches.Commands.UpdateCoachDetail;

public class UpdateCoachDetailCommand : IRequest<bool>
{
    public int CoachId { get; set; }
    public JsonPatchDocument<UpdateCoachCommand> Item { get; set; } = default!;
}