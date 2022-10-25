using System.ComponentModel.DataAnnotations;

namespace FootballStats.ApplicationModule.Common.DTOs.Players;

public class PlayerReadDTO
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Lastname { get; set; }

    [Required]
    public int Age { get; set; }

    [MaxLength(50)]
    public string? Club { get; set; }
}