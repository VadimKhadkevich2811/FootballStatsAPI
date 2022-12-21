using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Common.Wrappers.Links;
using FootballStats.Application.Trainings.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FootballStats.Application.Trainings;

public static class TrainingResponseWrapper
{
    public static void GenerateLinksForTraining(Response<TrainingReadDto> trainingResponse, LinkGenerator linkGenerator,
        HttpContext httpContext)
    {
        var trainingLinks = new List<Link>
        {
            new Link(linkGenerator.GetUriByAction(httpContext, "GetTrainingById"),
                "self",
                "GET"),
            new Link(linkGenerator.GetUriByAction(httpContext, "DeleteTraining", values: new {trainingId = trainingResponse.Data!.Id}),
                "delete_training",
                "DELETE"),
            new Link(linkGenerator.GetUriByAction(httpContext, "UpdateTraining", values: new {trainingId = trainingResponse.Data!.Id}),
                "update_training",
                "PUT"),
            new Link(linkGenerator.GetUriByAction(httpContext, "UpdateTrainingDetail", values: new {trainingId = trainingResponse.Data!.Id}),
                "patch_training",
                "PATCH")
        };

        trainingResponse.Data!.Links = trainingLinks;
    }

    public static void GenerateLinksForTrainings(Response<TrainingsListWithCountDto> trainingsResponse, LinkGenerator linkGenerator,
        HttpContext httpContext)
    {
        foreach (var training in trainingsResponse.Data!.TrainingsList)
        {
            var trainingLinks = new List<Link>
            {
                new Link(linkGenerator.GetUriByAction(httpContext, "GetTrainingById", values: new {trainingId = training.Id}),
                    "self",
                    "GET"),
                new Link(linkGenerator.GetUriByAction(httpContext, "DeleteTraining", values: new {trainingId = training.Id}),
                    "delete_training",
                    "DELETE"),
                new Link(linkGenerator.GetUriByAction(httpContext, "UpdateTraining", values: new {trainingId = training.Id}),
                    "update_training",
                    "PUT"),
                new Link(linkGenerator.GetUriByAction(httpContext, "UpdateTrainingDetail", values: new {trainingId = training.Id}),
                    "patch_training",
                    "PATCH")
            };

            training.Links = trainingLinks;
        }
    }
}