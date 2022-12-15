using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Common.Wrappers.Links;
using FootballStats.Application.Players.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FootballStats.Application.Players;

public static class PlayerResponseWrapper
{
    public static void GenerateLinksForPlayer(Response<PlayerReadDto> playerResponse, LinkGenerator linkGenerator,
        HttpContext httpContext)
    {
        var playerLinks = new List<Link>
        {
            new Link(linkGenerator.GetUriByAction(httpContext, "GetPlayerById"),
                "self",
                "GET"),
            new Link(linkGenerator.GetUriByAction(httpContext, "DeletePlayer", values: new {playerId = playerResponse.Data!.Id}),
                "delete_player",
                "DELETE"),
            new Link(linkGenerator.GetUriByAction(httpContext, "UpdatePlayer", values: new {playerId = playerResponse.Data!.Id}),
                "update_player",
                "PUT"),
            new Link(linkGenerator.GetUriByAction(httpContext, "UpdatePlayerDetail", values: new {playerId = playerResponse.Data!.Id}),
                "patch_player",
                "PATCH")
        };

        playerResponse.Data!.Links = playerLinks;
    }

    public static void GenerateLinksForPlayers(Response<PlayersListWithCountDto> playersResponse, LinkGenerator linkGenerator,
        HttpContext httpContext)
    {
        foreach (var player in playersResponse.Data!.PlayersList)
        {
            var playerLinks = new List<Link>
            {
                new Link(linkGenerator.GetUriByAction(httpContext, "GetPlayerById", values: new {playerId = player.Id}),
                    "self",
                    "GET"),
                new Link(linkGenerator.GetUriByAction(httpContext, "DeletePlayer", values: new {playerId = player.Id}),
                    "delete_player",
                    "DELETE"),
                new Link(linkGenerator.GetUriByAction(httpContext, "UpdatePlayer", values: new {playerId = player.Id}),
                    "update_player",
                    "PUT"),
                new Link(linkGenerator.GetUriByAction(httpContext, "UpdatePlayerDetail", values: new {playerId = player.Id}),
                    "patch_player",
                    "PATCH")
            };

            player.Links = playerLinks;
        }
    }
}