using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs;
using FootballStats.ApplicationModule.Login.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace FootballStats.ApplicationModule.Common.Login.Handlers;

public class LoginHandler : IRequestHandler<LoginCommand, int>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;
    public LoginHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = BC.HashPassword(request.Password);
        var user = await _context.Users
            .Where(user => (user.UserName == request.LoginId || user.Email == request.LoginId)
                && user.PasswordHash == passwordHash).FirstOrDefaultAsync();
        _mapper.Map<LoginDTO>(user);
        return 1;
    }
}