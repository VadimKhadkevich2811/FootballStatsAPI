using FootballStats.ApplicationModule.Coaches.Commands.UpdateCoach;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace FootballStats.ApplicationModule.Coaches.Commands.UpdateCoachDetail;

public class UpdateCoachDetailCommand : IRequest<Response<bool>>
{
    public int CoachId { get; set; }
    public JsonPatchDocument<UpdateCoachCommand> Item { get; set; } = default!;
}