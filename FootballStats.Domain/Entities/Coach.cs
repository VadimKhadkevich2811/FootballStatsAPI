using System.ComponentModel.DataAnnotations;

namespace FootballStats.Domain.Entities;

public class Coach
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [MaxLength(50)]
    public string Lastname { get; set; }

    [Required]
    public int Age { get; set; }

    public int TrainingForeignKey { get; set; }
    public Training Training { get; set; }
}