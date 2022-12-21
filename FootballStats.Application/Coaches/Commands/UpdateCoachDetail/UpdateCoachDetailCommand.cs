using FootballStats.Application.Coaches.Commands.UpdateCoach;
using FootballStats.Application.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace FootballStats.Application.Coaches.Commands.UpdateCoachDetail;

public class UpdateCoachDetailCommand : IRequest<Response<bool>>
{
    public int CoachId { get; set; }
    public JsonPatchDocument<UpdateCoachCommand> Item { get; set; } = default!;
}