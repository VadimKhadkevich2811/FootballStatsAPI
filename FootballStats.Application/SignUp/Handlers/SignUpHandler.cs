using AutoMapper;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.SignUp.Commands;
using FootballStats.Application.SignUp.Dtos;
using FootballStats.Domain.Entities;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace FootballStats.Application.Common.SignUp.Handlers;

public class SignUpHandler : IRequestHandler<SignUpCommand, Response<SignUpDto?>>
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

    public async Task<Response<SignUpDto?>> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = BC.HashPassword(request.Password);

        var userExist = _repository.UserExist(request.Email, request.Username);

        if (userExist)
        {
            string message = $"User with username ({request.Username}) or email ({request.Email}) already exists.";
            _logger.LogWarn(message);
            return new Response<SignUpDto?>(null, false, "Problem during signing up", message);
        }

        var user = new User()
        {
            Email = request.Email,
            UserName = request.Username,
            PasswordHash = passwordHash
        };

        await _repository.AddUserAsync(user);
        await _repository.SaveChangesAsync();

        var newUser = _mapper.Map<SignUpDto>(user);
        var newUserResponse = new Response<SignUpDto?>(newUser, true);

        return newUserResponse;
    }
}