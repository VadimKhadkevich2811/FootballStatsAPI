using FootballStats.ApplicationModule.Common.DTOs;
using MediatR;

namespace FootballStats.ApplicationModule.SignUp.Commands;

public class SignUpCommand : IRequest<SignUpDTO?>
{
    public string Email { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string RepeatPassword { get; set; } = default!;
}