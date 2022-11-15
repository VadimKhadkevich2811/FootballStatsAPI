using System.ComponentModel.DataAnnotations;

namespace FootballStats.ApplicationModule.Common.DTOs;

public class LoginDTO
{
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }

    [Required]
    public string Token { get; set; }
}