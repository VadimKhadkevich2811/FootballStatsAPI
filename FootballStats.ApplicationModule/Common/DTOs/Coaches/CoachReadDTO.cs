using System.ComponentModel.DataAnnotations;
using FootballStats.Domain.Enums;

namespace FootballStats.ApplicationModule.Common.DTOs.Coaches;

public class CoachReadDTO
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

    [Required]
    public PositionGroup Position { get; set; }
}