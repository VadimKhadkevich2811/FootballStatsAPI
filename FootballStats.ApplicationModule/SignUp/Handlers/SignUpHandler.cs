using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.SignUp.Commands;
using FootballStats.Domain.Entities;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace FootballStats.ApplicationModule.Common.SignUp.Handlers;

public class SignUpHandler : IRequestHandler<SignUpCommand, SignUpDTO>
{
    private readonly ISignUpRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _logger;
    public SignUpHandler(ISignUpRepository repository, IMapper mapper, ILoggerManager logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SignUpDTO> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = BC.HashPassword(request.Password);

        var userExist = _repository.UserExist(request.Email, request.Username);

        if (userExist)
        {
            _logger.LogWarn("User exists.");
            return null;
        }

        var user = new User()
        {
            Email = request.Email,
            UserName = request.Username,
            PasswordHash = passwordHash
        };

        await _repository.AddUserAsync(user);
        await _repository.SaveChangesAsync();

        var newUser = _mapper.Map<SignUpDTO>(user);
        return newUser;
    }
}