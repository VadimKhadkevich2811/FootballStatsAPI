using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Common.Wrappers.Links;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FootballStats.Application.Coaches;

public static class CoachResponseWrapper
{
    public static void GenerateLinksForCoach(Response<CoachReadDto> coachResponse, LinkGenerator linkGenerator,
        HttpContext httpContext)
    {
        var coachLinks = new List<Link>
        {
            new Link(linkGenerator.GetUriByAction(httpContext, "GetCoachById"),
                "self",
                "GET"),
            new Link(linkGenerator.GetUriByAction(httpContext, "DeleteCoach", values: new {coachId = coachResponse.Data!.Id}),
                "delete_coach",
                "DELETE"),
            new Link(linkGenerator.GetUriByAction(httpContext, "UpdateCoach", values: new {coachId = coachResponse.Data!.Id}),
                "update_coach",
                "PUT"),
            new Link(linkGenerator.GetUriByAction(httpContext, "UpdateCoachDetail", values: new {coachId = coachResponse.Data!.Id}),
                "patch_coach",
                "PATCH")
        };

        coachResponse.Data!.Links = coachLinks;
    }

    public static void GenerateLinksForCoaches(Response<CoachesListWithCountDto> coachesResponse, LinkGenerator linkGenerator,
        HttpContext httpContext)
    {
        foreach (var coach in coachesResponse.Data!.CoachesList)
        {
            var coachLinks = new List<Link>
            {
                new Link(linkGenerator.GetUriByAction(httpContext, "GetCoachById", values: new {coachId = coach.Id}),
                    "self",
                    "GET"),
                new Link(linkGenerator.GetUriByAction(httpContext, "DeleteCoach", values: new {coachId = coach.Id}),
                    "delete_coach",
                    "DELETE"),
                new Link(linkGenerator.GetUriByAction(httpContext, "UpdateCoach", values: new {coachId = coach.Id}),
                    "update_coach",
                    "PUT"),
                new Link(linkGenerator.GetUriByAction(httpContext, "UpdateCoachDetail", values: new {coachId = coach.Id}),
                    "patch_coach",
                    "PATCH")
            };

            coach.Links = coachLinks;
        }
    }
}