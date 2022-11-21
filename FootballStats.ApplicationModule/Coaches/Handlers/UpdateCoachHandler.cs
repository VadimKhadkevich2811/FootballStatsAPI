using AutoMapper;
using FootballStats.ApplicationModule.Coaches.Commands.UpdateCoach;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using MediatR;

namespace FootballStats.ApplicationModule.Common.Coaches.Handlers;

public class UpdateCoachHandler : IRequestHandler<UpdateCoachCommand, bool>
{
    private readonly ICoachesRepository _repository;
    private readonly IMapper _mapper;
    public UpdateCoachHandler(ICoachesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateCoachCommand request, CancellationToken cancellationToken)
    {
        var coach = await _repository.GetCoachByIdAsync(request.CoachId);

        if (coach == null)
        {
            return false;
        }

        _mapper.Map(request, coach);

        _repository.UpdateCoach(coach);
        return await _repository.SaveChangesAsync();
    }
}