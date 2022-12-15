using System.ComponentModel.DataAnnotations;
using FootballStats.Application.Common.Wrappers.Links;
using FootballStats.Application.Players.Dtos;

namespace FootballStats.Application.Trainings.Dtos;

public class TrainingReadDto : LinkResourceBase
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