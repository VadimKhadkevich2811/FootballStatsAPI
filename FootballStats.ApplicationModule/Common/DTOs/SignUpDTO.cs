using System.ComponentModel.DataAnnotations;

namespace FootballStats.ApplicationModule.Common.DTOs;

public class SignUpDTO
{
    [Required]
    [MaxLength(50)]
    public string Email { get; set; } = default!;

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = default!;

    [Required]
    public string PasswordHash { get; set; } = default!;
}