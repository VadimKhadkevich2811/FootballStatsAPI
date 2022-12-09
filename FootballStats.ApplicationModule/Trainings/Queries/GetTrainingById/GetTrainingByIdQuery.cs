using FootballStats.ApplicationModule.Common.Dtos.Trainings;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Queries.GetTrainingById;

public class GetTrainingByIdQuery : IRequest<Response<TrainingReadDto>>
{
    public int TrainingId { get; }

    public GetTrainingByIdQuery(int trainingId)
    {
        TrainingId = trainingId;
    }
}