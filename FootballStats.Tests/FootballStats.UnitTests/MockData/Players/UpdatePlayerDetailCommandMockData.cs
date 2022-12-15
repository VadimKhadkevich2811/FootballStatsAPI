using FootballStats.Application.Players.Commands.UpdatePlayer;
using FootballStats.Application.Players.Commands.UpdatePlayerDetail;
using Microsoft.AspNetCore.JsonPatch;

namespace FootballStats.UnitTests.MockData.Players;

public class UpdatePlayerDetailCommandMockData
{
    public static UpdatePlayerDetailCommand GetEmptyUpdatePlayerDetailCommandData()
    {
        return new UpdatePlayerDetailCommand();
    }

    public static UpdatePlayerDetailCommand GetTestUpdatePlayerDetailCommandData()
    {
        return new UpdatePlayerDetailCommand()
        {
            Item = new JsonPatchDocument<UpdatePlayerCommand>(),
            PlayerId = 1
        };
    }

}