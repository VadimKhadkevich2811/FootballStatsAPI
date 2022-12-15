using FootballStats.Application.Common.Wrappers;
using MediatR;

namespace FootballStats.Application.Trainings.Commands.UpdateTraining;

public class UpdateTrainingCommand : IRequest<Response<bool>>
{
    public int TrainingId { get; set; }
    public string Name { get; set; } = default!;
    public int CoachId { get; set; }
    public DateTime TrainingDate { get; set; }
    public ICollection<int> PlayerIds { get; set; } = default!;
}