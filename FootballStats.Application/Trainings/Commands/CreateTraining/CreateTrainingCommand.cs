using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Trainings.Dtos;
using MediatR;

namespace FootballStats.Application.Trainings.Commands.CreateTraining;

public class CreateTrainingCommand : IRequest<Response<TrainingReadDto>>
{
    public string Name { get; set; } = default!;
    public int CoachId { get; set; }
    public DateTime TrainingDate { get; set; }
    public ICollection<int> PlayerIDs { get; set; } = default!;
}