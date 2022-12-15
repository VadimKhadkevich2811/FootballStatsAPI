using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Login.Dtos;
using MediatR;

namespace FootballStats.Application.Login.Commands;

public class LoginCommand : IRequest<Response<LoginDto>>
{
    public string LoginId { get; set; } = default!;

    public string Password { get; set; } = default!;
}