using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Commands.DeleteTraining;

public class DeleteTrainingCommand : IRequest<bool>
{
    public int TrainingId { get; }

    public DeleteTrainingCommand(int trainingId)
    {
        TrainingId = trainingId;
    }
}