using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Common.Wrappers.Links;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FootballStats.Infrastructure.Services.ResponseWrappers;

public class CoachResponseWrapper : IResponseWrapper<CoachReadDto, CoachesListWithCountDto>
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _contextAccessor;
    public CoachResponseWrapper(LinkGenerator linkGenerator, IHttpContextAccessor contextAccessor)
    {
        _linkGenerator = linkGenerator;
        _contextAccessor = contextAccessor;
    }

    public void GenerateLinksForOne(Response<CoachReadDto> response)
    {
        var coachLinks = new List<Link>
        {
            new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "GetCoachById"),
                "self",
                "GET"),
            new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "DeleteCoach",
                values: new {coachId = response.Data!.Id}),
                "delete_coach",
                "DELETE"),
            new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "UpdateCoach",
                values: new {coachId = response.Data!.Id}),
                "update_coach",
                "PUT"),
            new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "UpdateCoachDetail",
                values: new {coachId = response.Data!.Id}),
                "patch_coach",
                "PATCH")
        };

        response.Data!.Links = coachLinks;
    }

    public void GenerateLinksForMany(Response<CoachesListWithCountDto> response)
    {
        foreach (var coach in response.Data!.CoachesList)
        {
            var coachLinks = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "GetCoachById",
                    values: new {coachId = coach.Id}),
                    "self",
                    "GET"),
                new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "DeleteCoach",
                    values: new {coachId = coach.Id}),
                    "delete_coach",
                    "DELETE"),
                new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "UpdateCoach",
                    values: new {coachId = coach.Id}),
                    "update_coach",
                    "PUT"),
                new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "UpdateCoachDetail",
                    values: new {coachId = coach.Id}),
                    "patch_coach",
                    "PATCH")
            };

            coach.Links = coachLinks;
        }
    }
}