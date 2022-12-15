using AutoMapper;
using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Coaches.Queries.GetAllCoaches;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using MediatR;

namespace FootballStats.Application.Coaches.Handlers;

public class GetAllCoachesHandler : IRequestHandler<GetAllCoachesQuery, Response<CoachesListWithCountDto>>
{
    private readonly ICoachesRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCoachesHandler(ICoachesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<CoachesListWithCountDto>> Handle(GetAllCoachesQuery request, CancellationToken cancellationToken)
    {
        var filter = request.CoachesQueryStringParams;
        var coaches = await _repository.GetAllCoachesAsync(filter);
        var coachesCount = await _repository.GetAllCoachesCountAsync();
        var coachDtos = _mapper.Map<List<CoachReadDto>>(coaches);
        var coachesResult = new CoachesListWithCountDto(coachDtos, coachesCount);
        var coachesResponse = new Response<CoachesListWithCountDto>(coachesResult, true);

        return coachesResponse;
    }
}