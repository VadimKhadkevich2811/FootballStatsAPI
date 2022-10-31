namespace FootballStats.ApplicationModule.Common.DTOs.Players;

public class PlayersListWithCountDTO
{
    public List<PlayerReadDTO> PlayersList { get; set; }
    public int PlayersTotalCount { get; set; }

    public PlayersListWithCountDTO(List<PlayerReadDTO> playersList, int playersTotalCount)
    {
        PlayersList = playersList;
        PlayersTotalCount = playersTotalCount;
    }
}