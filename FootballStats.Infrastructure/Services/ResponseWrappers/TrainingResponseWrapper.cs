using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Common.Wrappers.Links;
using FootballStats.Application.Trainings.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FootballStats.Infrastructure.Services.ResponseWrappers;

public class TrainingResponseWrapper : IResponseWrapper<TrainingReadDto, TrainingsListWithCountDto>
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _contextAccessor;
    public TrainingResponseWrapper(LinkGenerator linkGenerator, IHttpContextAccessor contextAccessor)
    {
        _linkGenerator = linkGenerator;
        _contextAccessor = contextAccessor;
    }

    public void GenerateLinksForOne(Response<TrainingReadDto> response)
    {
        var trainingLinks = new List<Link>
        {
            new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "GetTrainingById"),
                "self",
                "GET"),
            new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "DeleteTraining",
                values: new {trainingId = response.Data!.Id}),
                "delete_training",
                "DELETE"),
            new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "UpdateTraining",
                values: new {trainingId = response.Data!.Id}),
                "update_training",
                "PUT"),
            new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "UpdateTrainingDetail",
                values: new {trainingId = response.Data!.Id}),
                "patch_training",
                "PATCH")
        };

        response.Data!.Links = trainingLinks;
    }

    public void GenerateLinksForMany(Response<TrainingsListWithCountDto> response)
    {
        foreach (var training in response.Data!.TrainingsList)
        {
            var trainingLinks = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "GetTrainingById",
                    values: new {trainingId = training.Id}),
                    "self",
                    "GET"),
                new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "DeleteTraining",
                    values: new {trainingId = training.Id}),
                    "delete_training",
                    "DELETE"),
                new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "UpdateTraining",
                    values: new {trainingId = training.Id}),
                    "update_training",
                    "PUT"),
                new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "UpdateTrainingDetail",
                    values: new {trainingId = training.Id}),
                    "patch_training",
                    "PATCH")
            };

            training.Links = trainingLinks;
        }
    }
}