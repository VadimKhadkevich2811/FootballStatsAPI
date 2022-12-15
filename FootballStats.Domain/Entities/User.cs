using System.ComponentModel.DataAnnotations;
using FootballStats.Domain.Common;
using FootballStats.Domain.Enums;

namespace FootballStats.Domain.Entities;

public class User : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Email { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = default!;

    [Required]
    public string PasswordHash { get; set; } = default!;

    [Required]
    public Role UserRole { get; set; }

    public string? Token { get; set; }

    public DateTime? TokenEnd { get; set; }
}