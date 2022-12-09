using AutoMapper;
using FootballStats.ApplicationModule.Coaches.Queries.GetCoachById;
using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Handlers;

public class GetCoachByIdHandler : IRequestHandler<GetCoachByIdQuery, Response<CoachReadDto>>
{
    private readonly ICoachesRepository _repository;
    private readonly IMapper _mapper;

    public GetCoachByIdHandler(ICoachesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<CoachReadDto>> Handle(GetCoachByIdQuery request, CancellationToken cancellationToken)
    {
        var coach = await _repository.GetCoachByIdAsync(request.CoachId);
        var coachDto = _mapper.Map<CoachReadDto>(coach);

        return new Response<CoachReadDto>(coachDto, true, null, coachDto == null
            ? $"No Coach Found By Id = {request.CoachId}"
            : null);
    }
}