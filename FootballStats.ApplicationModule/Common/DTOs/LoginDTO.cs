using System.ComponentModel.DataAnnotations;

namespace FootballStats.ApplicationModule.Common.Dtos;

public class LoginDto
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = default!;

    [Required]
    public string Token { get; set; } = default!;
}