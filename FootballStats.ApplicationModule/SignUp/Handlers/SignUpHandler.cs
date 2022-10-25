using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs;
using FootballStats.ApplicationModule.SignUp.Commands;
using FootballStats.Domain.Entities;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace FootballStats.ApplicationModule.Common.SignUp.Handlers;

public class SignUpHandler : IRequestHandler<SignUpCommand, SignUpDTO>
{
    private IApplicationDbContext _context;
    private IMapper _mapper;
    public SignUpHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SignUpDTO> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = BC.HashPassword(request.Password);

        var userExists = _context.Users
            .Any(user => user.Email == request.Email
            || user.UserName == request.Username);

        if (userExists)
        {
            return null;
        }

        var user = new User()
        {
            Email = request.Email,
            UserName = request.Username,
            PasswordHash = passwordHash
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        var newUser = _mapper.Map<SignUpDTO>(user);
        return newUser;
    }
}