using FootballStats.Application.Common.Wrappers;
using MediatR;

namespace FootballStats.Application.Trainings.Commands.DeleteTraining;

public class DeleteTrainingCommand : IRequest<Response<bool>>
{
    public int TrainingId { get; }

    public DeleteTrainingCommand(int trainingId)
    {
        TrainingId = trainingId;
    }
}