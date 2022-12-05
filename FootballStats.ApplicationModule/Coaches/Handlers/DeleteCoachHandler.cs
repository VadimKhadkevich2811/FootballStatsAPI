using AutoMapper;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using MediatR;
using FootballStats.ApplicationModule.Coaches.Commands.DeleteCoach;

namespace FootballStats.ApplicationModule.Coaches.Handlers;

public class DeleteCoachHandler : IRequestHandler<DeleteCoachCommand, bool>
{
    private readonly ICoachesRepository _repository;
    private readonly IMapper _mapper;

    public DeleteCoachHandler(ICoachesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteCoachCommand request, CancellationToken cancellationToken)
    {
        var coach = await _repository.GetCoachByIdAsync(request.CoachId);

        if (coach == null)
        {
            return false;
        }

        _repository.RemoveCoach(coach);
        
        return await _repository.SaveChangesAsync();
    }
}