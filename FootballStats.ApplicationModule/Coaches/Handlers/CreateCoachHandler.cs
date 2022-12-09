using AutoMapper;
using FootballStats.ApplicationModule.Coaches.Commands.CreateCoach;
using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Wrappers;
using FootballStats.Domain.Entities;
using MediatR;

namespace FootballStats.ApplicationModule.Common.Coaches.Handlers;

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