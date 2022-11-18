using AutoMapper;
using FootballStats.ApplicationModule.Coaches.Commands.CreateCoach;
using FootballStats.ApplicationModule.Common.DTOs.Coaches;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.Domain.Entities;
using MediatR;

namespace FootballStats.ApplicationModule.Common.Coachs.Handlers;

public class CreateCoachHandler : IRequestHandler<CreateCoachCommand, CoachReadDTO>
{
    private readonly ICoachesRepository _repository;
    private readonly IMapper _mapper;
    public CreateCoachHandler(ICoachesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CoachReadDTO> Handle(CreateCoachCommand request, CancellationToken cancellationToken)
    {
        var coach = _mapper.Map<Coach>(request);
        await _repository.AddCoach(coach);
        await _repository.SaveChangesAsync();

        var newCoach = _mapper.Map<CoachReadDTO>(coach);

        return newCoach;
    }
}