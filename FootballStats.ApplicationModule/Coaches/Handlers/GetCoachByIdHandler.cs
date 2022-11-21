using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Coaches;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Trainings.Queries.GetCoachById;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Handlers;

public class GetCoachByIdHandler : IRequestHandler<GetCoachByIdQuery, CoachReadDTO>
{
    private readonly ICoachesRepository _repository;
    private readonly IMapper _mapper;

    public GetCoachByIdHandler(ICoachesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CoachReadDTO> Handle(GetCoachByIdQuery request, CancellationToken cancellationToken)
    {
        var coach = await _repository.GetCoachByIdAsync(request.CoachId);
        var coachDTO = _mapper.Map<CoachReadDTO>(coach);

        return coachDTO;
    }
}