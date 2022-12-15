namespace FootballStats.Application.Trainings.Dtos;

public class TrainingsListWithCountDto
{
    public List<TrainingReadDto> TrainingsList { get; set; }
    public int TrainingsTotalCount { get; set; }

    public TrainingsListWithCountDto(List<TrainingReadDto> trainingsList, int trainingsTotalCount)
    {
        TrainingsList = trainingsList;
        TrainingsTotalCount = trainingsTotalCount;
    }
}