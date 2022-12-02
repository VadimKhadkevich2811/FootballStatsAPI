using AutoMapper;
using FootballStats.ApplicationModule.Coaches.Queries.GetFreeCoaches;
using FootballStats.ApplicationModule.Common.DTOs.Coaches;
using FootballStats.ApplicationModule.Common.DTOs.Players;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Players.Queries.GetFreePlayers;
using MediatR;

namespace FootballStats.ApplicationModule.Players.Handlers;

public class GetFreeCoachesHandler : IRequestHandler<GetFreeCoachesQuery, CoachesListWithCountDTO>
{
    private readonly ICoachesRepository _repository;
    private readonly IMapper _mapper;

    public GetFreeCoachesHandler(ICoachesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CoachesListWithCountDTO> Handle(GetFreeCoachesQuery request, CancellationToken cancellationToken)
    {
        var date = request.Date;
        var freeCoaches = await _repository.GetFreeCoachesByDateAsync(date);
        var freeCoachesCount = await _repository.GetFreeCoachesByDateCountAsync(date);
        var freeCoachDTOs = _mapper.Map<List<CoachReadDTO>>(freeCoaches);

        return new CoachesListWithCountDTO(freeCoachDTOs, freeCoachesCount);
    }
}