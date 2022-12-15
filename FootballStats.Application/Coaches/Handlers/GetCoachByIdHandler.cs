using AutoMapper;
using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Coaches.Queries.GetCoachById;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using MediatR;

namespace FootballStats.Application.Coaches.Handlers;

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

        return coachDto == null
            ? new Response<CoachReadDto>(null, false, "Error during getting a coach by id",
                $"No Coach Found By Id = {request.CoachId}")
            : new Response<CoachReadDto>(coachDto);
    }
}