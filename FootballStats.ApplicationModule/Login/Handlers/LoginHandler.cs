using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Login.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace FootballStats.ApplicationModule.Common.Login.Handlers;

public class LoginHandler : IRequestHandler<LoginCommand, LoginDTO>
{
    private readonly ILoginRepository _repository;
    private readonly IAuthentication _auth;
    private readonly IMapper _mapper;
    public LoginHandler(ILoginRepository repository, IMapper mapper, IAuthentication auth)
    {
        _repository = repository;
        _mapper = mapper;
        _auth = auth;
    }

    public async Task<LoginDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByEmailOrUsername(request.LoginId);

        if (user != null && !BC.Verify(request.Password, user.PasswordHash))
        {
            user = null;
        }

        if (user != null)
        {
            if (user.Token == null)
            {
                var token = await _auth.GetAuthenticationToken();
                await _repository.UpdateUserToken(user, token);
            }
        }

        var userResponse = _mapper.Map<LoginDTO>(user);
        return userResponse;
    }
}