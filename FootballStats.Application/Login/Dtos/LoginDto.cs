using System.ComponentModel.DataAnnotations;

namespace FootballStats.Application.Login.Dtos;

public class LoginDto
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = default!;

    [Required]
    public string Token { get; set; } = default!;
}