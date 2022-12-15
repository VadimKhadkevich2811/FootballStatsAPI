using System.ComponentModel.DataAnnotations;
using FootballStats.Application.Common.Wrappers.Links;
using FootballStats.Domain.Enums;

namespace FootballStats.Application.Coaches.Dtos;

public class CoachReadDto : LinkResourceBase
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
    public string Nationality { get; set; } = default!;

    [Required]
    public PositionGroup Position { get; set; }
}