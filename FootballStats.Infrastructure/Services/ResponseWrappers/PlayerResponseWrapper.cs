using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Common.Wrappers.Links;
using FootballStats.Application.Players.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FootballStats.Infrastructure.Services.ResponseWrappers;

public class PlayerResponseWrapper : IResponseWrapper<PlayerReadDto, PlayersListWithCountDto>
{
    private readonly LinkGenerator _linkGenerator;
    private readonly IHttpContextAccessor _contextAccessor;
    public PlayerResponseWrapper(LinkGenerator linkGenerator, IHttpContextAccessor contextAccessor)
    {
        _linkGenerator = linkGenerator;
        _contextAccessor = contextAccessor;
    }

    public void GenerateLinksForOne(Response<PlayerReadDto> response)
    {
        var playerLinks = new List<Link>
        {
            new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "GetPlayerById"),
                "self",
                "GET"),
            new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "DeletePlayer",
                values: new {playerId = response.Data!.Id}),
                "delete_player",
                "DELETE"),
            new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "UpdatePlayer",
                values: new {playerId = response.Data!.Id}),
                "update_player",
                "PUT"),
            new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "UpdatePlayerDetail",
                values: new {playerId = response.Data!.Id}),
                "patch_player",
                "PATCH")
        };

        response.Data!.Links = playerLinks;
    }

    public void GenerateLinksForMany(Response<PlayersListWithCountDto> response)
    {
        foreach (var player in response.Data!.PlayersList)
        {
            var playerLinks = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "GetPlayerById",
                    values: new {playerId = player.Id}),
                    "self",
                    "GET"),
                new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "DeletePlayer",
                    values: new {playerId = player.Id}),
                    "delete_player",
                    "DELETE"),
                new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "UpdatePlayer",
                    values: new {playerId = player.Id}),
                    "update_player",
                    "PUT"),
                new Link(_linkGenerator.GetUriByAction(_contextAccessor.HttpContext!, "UpdatePlayerDetail",
                    values: new {playerId = player.Id}),
                    "patch_player",
                    "PATCH")
            };

            player.Links = playerLinks;
        }
    }
}