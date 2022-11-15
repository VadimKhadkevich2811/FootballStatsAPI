using System.ComponentModel.DataAnnotations;
using FootballStats.Domain.Enums;

namespace FootballStats.ApplicationModule.Common.DTOs.Trainings;

public class TrainingReadDTO
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    public int CoachId { get; set; }

    public PositionGroup position { get; set; }
}