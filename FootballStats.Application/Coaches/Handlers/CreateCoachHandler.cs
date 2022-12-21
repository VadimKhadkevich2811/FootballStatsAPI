using AutoMapper;
using FootballStats.Application.Coaches.Commands.CreateCoach;
using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Domain.Entities;
using MediatR;

namespace FootballStats.Application.Common.Coaches.Handlers;

public class CreateCoachHandler : IRequestHandler<CreateCoachCommand, Response<CoachReadDto>>
{
    private readonly ICoachesRepository _repository;
    private readonly IMapper _mapper;
    public CreateCoachHandler(ICoachesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<CoachReadDto>> Handle(CreateCoachCommand request, CancellationToken cancellationToken)
    {
        var coach = _mapper.Map<Coach>(request);
        await _repository.AddCoachAsync(coach);
        await _repository.SaveChangesAsync();

        var newCoach = _mapper.Map<CoachReadDto>(coach);

        var newCoachResponse = new Response<CoachReadDto>(newCoach, true);

        return newCoachResponse;
    }
}