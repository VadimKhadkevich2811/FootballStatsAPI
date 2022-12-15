using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Common.Wrappers;
using MediatR;

namespace FootballStats.Application.Coaches.Queries.GetCoachById;

public class GetCoachByIdQuery : IRequest<Response<CoachReadDto>>
{
    public int CoachId { get; }

    public GetCoachByIdQuery(int coachId)
    {
        CoachId = coachId;
    }
}