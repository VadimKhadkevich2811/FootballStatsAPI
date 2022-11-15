using System.ComponentModel.DataAnnotations;

namespace FootballStats.ApplicationModule.Common.DTOs;

public class SignUpDTO
{
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }

    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }

    [Required]
    public string PasswordHash { get; set; }
}