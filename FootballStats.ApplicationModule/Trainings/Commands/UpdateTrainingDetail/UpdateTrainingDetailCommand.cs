using FootballStats.ApplicationModule.Trainings.Commands.UpdateTraining;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace FootballStats.ApplicationModule.Trainings.Commands.UpdateTrainingDetail;

public class UpdateTrainingDetailCommand : IRequest<bool>
{
    public int TrainingId { get; set; }
    public JsonPatchDocument<UpdateTrainingCommand> Item { get; set; } = default!;
}