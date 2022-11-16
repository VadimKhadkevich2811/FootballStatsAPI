using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Commands.UpdateTraining;

public class UpdateTrainingCommand : IRequest<bool>
{
    public int TrainingId { get; set; }
    public string Name { get; set; }
    public int CoachId { get; set; }
    public ICollection<int> PlayerIds { get; set; }
}