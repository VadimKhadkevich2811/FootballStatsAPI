using FootballStats.Application.Coaches.Commands.DeleteCoach;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using MediatR;

namespace FootballStats.Application.Coaches.Handlers;

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
            return new Response<bool>(false, false, "Error during deleting a coach",
                $"No coaches found with ID = {request.CoachId}");
        }

        _repository.RemoveCoach(coach);

        return new Response<bool>(await _repository.SaveChangesAsync(), true);
    }
}