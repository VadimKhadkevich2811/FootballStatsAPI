using FootballStats.ApplicationModule.Common.DTOs.Trainings;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Queries.GetTrainingById;

public class GetTrainingByIdQuery : IRequest<TrainingReadDTO>
{
    public int TrainingId { get; }

    public GetTrainingByIdQuery(int trainingId)
    {
        TrainingId = trainingId;
    }
}