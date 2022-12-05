using AutoMapper;
using FootballStats.ApplicationModule.Coaches.Commands.UpdateCoach;
using FootballStats.ApplicationModule.Coaches.Commands.UpdateCoachDetail;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using MediatR;

namespace FootballStats.ApplicationModule.Common.Coaches.Handlers;

public class UpdateCoachDetailHandler : IRequestHandler<UpdateCoachDetailCommand, bool>
{
    private readonly ICoachesRepository _repository;
    private readonly IMapper _mapper;
    public UpdateCoachDetailHandler(ICoachesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateCoachDetailCommand request, CancellationToken cancellationToken)
    {
        var coach = await _repository.GetCoachByIdAsync(request.CoachId);

        if (coach == null)
        {
            return false;
        }

        var coachToPatch = _mapper.Map<UpdateCoachCommand>(coach);
        request.Item.ApplyTo(coachToPatch);

        _mapper.Map(coachToPatch, coach);

        _repository.UpdateCoach(coach);
        
        return await _repository.SaveChangesAsync();
    }
}