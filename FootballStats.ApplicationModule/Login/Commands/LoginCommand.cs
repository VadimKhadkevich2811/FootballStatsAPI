using FootballStats.ApplicationModule.Common.DTOs;
using MediatR;

namespace FootballStats.ApplicationModule.Login.Commands;

public class LoginCommand : IRequest<int>
{
    public string LoginId { get; set; }

    public string Password { get; set; }
}