namespace FootballStats.ApplicationModule.Common.DTOs.Coaches;

public class CoachesListWithCountDTO
{
    public List<CoachReadDTO> CoachesList { get; set; }
    public int CoachesTotalCount { get; set; }

    public CoachesListWithCountDTO(List<CoachReadDTO> coachesList, int coachesTotalCount)
    {
        CoachesList = coachesList;
        CoachesTotalCount = coachesTotalCount;
    }
}