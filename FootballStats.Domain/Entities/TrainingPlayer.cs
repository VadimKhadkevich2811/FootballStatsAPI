using System.ComponentModel.DataAnnotations;

namespace FootballStats.Domain.Entities;

public class TrainingPlayer
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int PlayerId { get; set; }
    public Player Player { get; set; }

    [Required]
    public int TrainingId { get; set; }
    public Training Training { get; set; }
}