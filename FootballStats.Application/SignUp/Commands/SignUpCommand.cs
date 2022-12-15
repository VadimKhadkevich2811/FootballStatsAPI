using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.SignUp.Dtos;
using MediatR;

namespace FootballStats.Application.SignUp.Commands;

public class SignUpCommand : IRequest<Response<SignUpDto?>>
{
    public string Email { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string RepeatPassword { get; set; } = default!;
}