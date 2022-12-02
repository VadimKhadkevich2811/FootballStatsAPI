using System.ComponentModel.DataAnnotations;

namespace FootballStats.Domain.Entities;

public class Training
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
    
    public Coach? Coach { get; set; }

    public virtual ICollection<TrainingPlayer>? TrainingPlayers { get; set; }
}