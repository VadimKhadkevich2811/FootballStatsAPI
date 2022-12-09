using AutoMapper;
using FootballStats.ApplicationModule.Coaches.Commands.DeleteCoach;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;

namespace FootballStats.ApplicationModule.Coaches.Handlers;

public class DeleteCoachHandler : IRequestHandler<DeleteCoachCommand, Response<bool>>
{
    private readonly ICoachesRepository _repository;

    public DeleteCoachHandler(ICoachesRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<bool>> Handle(DeleteCoachCommand request, CancellationToken cancellationToken)
    {
        var coach = await _repository.GetCoachByIdAsync(request.CoachId);

        if (coach == null)
        {
            return new Response<bool>(false, false, null, $"No coaches found with ID = {request.CoachId}");
        }

        _repository.RemoveCoach(coach);

        return new Response<bool>(await _repository.SaveChangesAsync(), true);
    }
}