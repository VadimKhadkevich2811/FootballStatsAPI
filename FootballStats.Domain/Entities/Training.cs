using System.ComponentModel.DataAnnotations;

namespace FootballStats.Domain.Entities;

public class Training
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public int CoachId { get; set; }
    public Coach Coach { get; set; }

    public virtual ICollection<TrainingPlayer> TrainingPlayers { get; set; }
}