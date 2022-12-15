using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Trainings.Dtos;
using MediatR;

namespace FootballStats.Application.Trainings.Queries.GetTrainingById;

public class GetTrainingByIdQuery : IRequest<Response<TrainingReadDto>>
{
    public int TrainingId { get; }

    public GetTrainingByIdQuery(int trainingId)
    {
        TrainingId = trainingId;
    }
}