namespace FootballStats.Application.Players.Dtos;

public class PlayersListWithCountDto
{
    public List<PlayerReadDto> PlayersList { get; set; }
    public int PlayersTotalCount { get; set; }

    public PlayersListWithCountDto(List<PlayerReadDto> playersList, int playersTotalCount)
    {
        PlayersList = playersList;
        PlayersTotalCount = playersTotalCount;
    }
}