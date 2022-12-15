using FootballStats.ApplicationModule.Common.Dtos;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Login.Commands;

public class LoginCommand : IRequest<Response<LoginDto>>
{
    public string LoginId { get; set; } = default!;

    public string Password { get; set; } = default!;
}