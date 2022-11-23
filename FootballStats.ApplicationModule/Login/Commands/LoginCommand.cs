using FootballStats.ApplicationModule.Common.DTOs;
using MediatR;

namespace FootballStats.ApplicationModule.Login.Commands;

public class LoginCommand : IRequest<LoginDTO>
{
    public string LoginId { get; set; } = default!;

    public string Password { get; set; } = default!;
}