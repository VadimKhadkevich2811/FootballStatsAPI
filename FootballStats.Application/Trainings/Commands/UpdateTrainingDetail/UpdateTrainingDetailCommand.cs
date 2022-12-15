using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Trainings.Commands.UpdateTraining;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace FootballStats.Application.Trainings.Commands.UpdateTrainingDetail;

public class UpdateTrainingDetailCommand : IRequest<Response<bool>>
{
    public int TrainingId { get; set; }
    public JsonPatchDocument<UpdateTrainingCommand> Item { get; set; } = default!;
}