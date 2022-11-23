using System.ComponentModel.DataAnnotations;
using FootballStats.Domain.Enums;

namespace FootballStats.ApplicationModule.Common.DTOs.Players;

public class PlayerReadDTO
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string Lastname { get; set; } = default!;

    [Required]
    public int Age { get; set; }

    [Required]
    public PositionGroup Position { get; set; }
}