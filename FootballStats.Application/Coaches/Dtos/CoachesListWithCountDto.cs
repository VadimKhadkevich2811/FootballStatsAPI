namespace FootballStats.Application.Coaches.Dtos;

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