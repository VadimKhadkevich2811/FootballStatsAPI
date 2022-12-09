using FootballStats.ApplicationModule.Common.Dtos.Trainings;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Commands.CreateTraining;

public class CreateTrainingCommand : IRequest<Response<TrainingReadDto>>
{
    public string Name { get; set; } = default!;
    public int CoachId { get; set; }
    public DateTime TrainingDate { get; set; }
    public ICollection<int> PlayerIDs { get; set; } = default!;
}