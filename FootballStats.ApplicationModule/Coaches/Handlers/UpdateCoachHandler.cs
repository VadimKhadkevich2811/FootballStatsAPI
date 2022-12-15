using AutoMapper;
using FootballStats.ApplicationModule.Coaches.Commands.UpdateCoach;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Common.Coaches.Handlers;

public class UpdateCoachHandler : IRequestHandler<UpdateCoachCommand, Response<bool>>
{
    private readonly ICoachesRepository _repository;
    private readonly IMapper _mapper;
    public UpdateCoachHandler(ICoachesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Response<bool>> Handle(UpdateCoachCommand request, CancellationToken cancellationToken)
    {
        var coach = await _repository.GetCoachByIdAsync(request.CoachId);

        if (coach == null)
        {
            return new Response<bool>(false, false, null, $"No coaches found with ID = {request.CoachId}");
        }

        _mapper.Map(request, coach);

        _repository.UpdateCoach(coach);

        return new Response<bool>(await _repository.SaveChangesAsync(), true);
    }
}