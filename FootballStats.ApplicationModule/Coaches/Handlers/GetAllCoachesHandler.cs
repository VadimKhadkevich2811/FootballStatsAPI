using AutoMapper;
using FootballStats.ApplicationModule.Common.DTOs.Coaches;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Trainings.Queries.GetAllCoachesQuery;
using MediatR;

namespace FootballStats.ApplicationModule.Trainings.Handlers;

public class GetAllCoachesHandler : IRequestHandler<GetAllCoachesQuery, CoachesListWithCountDTO>
{
    private readonly ICoachesRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCoachesHandler(ICoachesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<CoachesListWithCountDTO> Handle(GetAllCoachesQuery request, CancellationToken cancellationToken)
    {
        var filter = request.CoachesQueryStringParams;
        var coaches = await _repository.GetAllCoachesAsync(filter.PageNumber, filter.PageSize);
        var coachesCount = await _repository.GetAllCoachesCountAsync();
        var coachDTOs = _mapper.Map<List<CoachReadDTO>>(coaches);

        return new CoachesListWithCountDTO(coachDTOs, coachesCount);
    }
}