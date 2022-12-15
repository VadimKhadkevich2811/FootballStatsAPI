using System.ComponentModel.DataAnnotations;
using FootballStats.Domain.Common;

namespace FootballStats.Domain.Entities;

public class Training : BaseEntity
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

    public virtual Coach? Coach { get; set; }

    public virtual ICollection<TrainingPlayer>? TrainingPlayers { get; set; }
}