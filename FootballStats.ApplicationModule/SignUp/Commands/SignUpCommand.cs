using FootballStats.ApplicationModule.Common.DTOs;
using MediatR;

namespace FootballStats.ApplicationModule.SignUp.Commands;

public class SignUpCommand : IRequest<SignUpDTO>
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
}