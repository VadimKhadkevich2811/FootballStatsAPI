using FootballStats.ApplicationModule.Common.Dtos;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.SignUp.Commands;

public class SignUpCommand : IRequest<Response<SignUpDto?>>
{
    public string Email { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string RepeatPassword { get; set; } = default!;
}