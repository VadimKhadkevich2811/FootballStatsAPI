namespace FootballStats.ApplicationModule.Common.DTOs.Trainings;

public class TrainingsListWithCountDTO
{
    public List<TrainingReadDTO> TrainingsList { get; set; }
    public int TrainingsTotalCount { get; set; }

    public TrainingsListWithCountDTO(List<TrainingReadDTO> trainingsList, int trainingsTotalCount)
    {
        TrainingsList = trainingsList;
        TrainingsTotalCount = trainingsTotalCount;
    }
}