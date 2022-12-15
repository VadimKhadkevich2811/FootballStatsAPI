using System.ComponentModel.DataAnnotations;
using FootballStats.ApplicationModule.Common.Dtos.Players;

namespace FootballStats.ApplicationModule.Common.Dtos.Trainings;

public class TrainingReadDto
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = default!;

    [Required]
    public int CoachId { get; set; }

    [Required]
    public DateTime TrainingDate { get; set; }

    [Required]
    public ICollection<PlayerReadDto> TrainedPlayers { get; set; } = default!;
}