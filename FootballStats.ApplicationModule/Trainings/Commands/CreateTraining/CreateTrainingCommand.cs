using FootballStats.ApplicationModule.Common.DTOs.Trainings;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Commands.CreateTraining;

public class CreateTrainingCommand : IRequest<TrainingReadDTO?>
{
    public string Name { get; set; } = default!;
    public int CoachId { get; set; }
    public DateTime TrainingDate { get; set; }
    public ICollection<int> PlayerIDs { get; set; } = default!;
}