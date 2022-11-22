using System.ComponentModel.DataAnnotations;

namespace FootballStats.ApplicationModule.Common.DTOs;

public class LoginDTO
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; }

    [Required]
    public string Token { get; set; }
}