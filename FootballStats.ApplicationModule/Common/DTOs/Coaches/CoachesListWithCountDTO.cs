namespace FootballStats.ApplicationModule.Common.Dtos.Coaches;

public class CoachesListWithCountDto
{
    public List<CoachReadDto> CoachesList { get; set; }
    public int CoachesTotalCount { get; set; }

    public CoachesListWithCountDto(List<CoachReadDto> coachesList, int coachesTotalCount)
    {
        CoachesList = coachesList;
        CoachesTotalCount = coachesTotalCount;
    }
}