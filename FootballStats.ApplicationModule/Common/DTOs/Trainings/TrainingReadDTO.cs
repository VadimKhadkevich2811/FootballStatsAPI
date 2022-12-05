using System.ComponentModel.DataAnnotations;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.Domain.Entities;

namespace FootballStats.ApplicationModule.Common.DTOs.Trainings;

public class TrainingReadDTO
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
    public ICollection<PlayerReadDTO> TrainedPlayers { get; set; } = default!;
}