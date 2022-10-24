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
    private IApplicationDbContext _context;
    private IAuthentication _auth;
    private IMapper _mapper;
    public LoginHandler(IApplicationDbContext context, IMapper mapper, IAuthentication auth)
    {
        _context = context;
        _mapper = mapper;
        _auth = auth;
    }

    public async Task<LoginDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Where(user => user.UserName == request.LoginId ||
                user.Email == request.LoginId).FirstOrDefaultAsync();

        if (user != null && !BC.Verify(request.Password, user.PasswordHash))
        {
            user = null;
        }

        if (user != null)
        {
            if (user.Token == null)
            {
                var token = await _auth.GetAuthenticationToken();
                if (token != null)
                {
                    user.Token = token;
                    user.TokenEnd = DateTime.Now.AddSeconds(86400);
                    await _context.SaveChangesAsync();
                }
            }
        }

        var userResponse = _mapper.Map<LoginDTO>(user);
        return userResponse;
    }
}