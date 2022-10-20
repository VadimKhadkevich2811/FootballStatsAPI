using System.ComponentModel.DataAnnotations;
using FootballStats.Domain.Enums;

namespace FootballStats.Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Email { get; set; }

    [Required]
    [MaxLength(50)]
    public string? UserName { get; set; }

    [Required]
    public string? PasswordHash { get; set; }

    [Required]
    public Role UserRole { get; set; }
}