using FootballStats.Domain.Entities;

namespace FootballStats.UnitTests.MockData.Trainings;

public class GetTrainingsMockData
{
    public static IEnumerable<Training> GetAllTrainings()
    {
        return new List<Training>()
        {
            new() { Id = 1, Name = "Name1", CoachId = 1, TrainingDate = DateTime.Now },
            new() { Id = 2, Name = "Name2", CoachId = 2, TrainingDate = DateTime.Now },
            new() { Id = 3, Name = "Name3", CoachId = 3, TrainingDate = DateTime.Now },
        };
    }

}