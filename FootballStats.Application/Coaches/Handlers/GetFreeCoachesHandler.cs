using AutoMapper;
using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Coaches.Queries.GetFreeCoaches;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using MediatR;

namespace FootballStats.Application.Players.Handlers;

public class GetFreeCoachesHandler : IRequestHandler<GetFreeCoachesQuery, Response<CoachesListWithCountDto>>
{
    private readonly ICoachesRepository _repository;
    private readonly IMapper _mapper;

    public GetFreeCoachesHandler(ICoachesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<CoachesListWithCountDto>> Handle(GetFreeCoachesQuery request, CancellationToken cancellationToken)
    {
        var date = request.Date;
        var freeCoaches = await _repository.GetFreeCoachesByDateAsync(date);
        var freeCoachesCount = await _repository.GetFreeCoachesByDateCountAsync(date);
        var freeCoachDtos = _mapper.Map<List<CoachReadDto>>(freeCoaches);

        var freeCoachesResult = new CoachesListWithCountDto(freeCoachDtos, freeCoachesCount);
        var freeCoachesResponse = new Response<CoachesListWithCountDto>(freeCoachesResult, true);

        return freeCoachesResponse;
    }
}