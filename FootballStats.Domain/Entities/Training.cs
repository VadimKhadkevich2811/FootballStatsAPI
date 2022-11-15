using System.ComponentModel.DataAnnotations;
using FootballStats.Domain.Enums;

namespace FootballStats.Domain.Entities;

public class Training
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public Coach Coach { get; set; }

    public PositionGroup Position { get; set; }

    public virtual ICollection<TrainingPlayer> TrainingPlayers { get; set; }
}