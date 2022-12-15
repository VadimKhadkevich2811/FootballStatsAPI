using System.Security.Claims;
using AutoMapper;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Login.Commands;
using FootballStats.Application.Login.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using BC = BCrypt.Net.BCrypt;

namespace FootballStats.Application.Common.Login.Handlers;

public class LoginHandler : IRequestHandler<LoginCommand, Response<LoginDto>>
{
    private readonly ILoginRepository _repository;
    private readonly IAuthentication _auth;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMapper _mapper;

    public LoginHandler(ILoginRepository repository, IMapper mapper,
        IAuthentication auth, IHttpContextAccessor contextAccessor)
    {
        _repository = repository;
        _mapper = mapper;
        _auth = auth;
        _contextAccessor = contextAccessor;
    }

    public async Task<Response<LoginDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByEmailOrUsernameAsync(request.LoginId);

        if (user != null && !BC.Verify(request.Password, user.PasswordHash))
        {
            return new Response<LoginDto>(null, true, null, "Login failed. Invalid username or password.");
        }

        if (user != null && (user.Token == null || DateTime.Now > user.TokenEnd))
        {
            var token = await _auth.GetAuthenticationTokenAsync();
            _repository.UpdateUserTokenAsync(user, token);
            await _repository.SaveChangesAsync();
        }

        var mappedUser = _mapper.Map<LoginDto>(user);

        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, mappedUser.Username),
                new Claim(ClaimTypes.Sid, mappedUser.Token)
            };

        var claimsIdentity = new ClaimsIdentity(
            claims, "auth");

        await _contextAccessor.HttpContext.SignInAsync("auth",
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties()
            {
                IsPersistent = true
            });

        var userResponse = new Response<LoginDto>(mappedUser, true);

        return userResponse;
    }
}