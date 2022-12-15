using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Commands.DeleteTraining;

public class DeleteTrainingCommand : IRequest<Response<bool>>
{
    public int TrainingId { get; }

    public DeleteTrainingCommand(int trainingId)
    {
        TrainingId = trainingId;
    }
}