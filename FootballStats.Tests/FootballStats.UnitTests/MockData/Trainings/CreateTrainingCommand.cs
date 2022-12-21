using FootballStats.Application.Trainings.Commands.CreateTraining;

namespace FootballStats.UnitTests.MockData.Trainings;

public class CreateTrainingCommandMockData
{
    public static CreateTrainingCommand GetEmptyCreateTrainingCommandData()
    {
        return new CreateTrainingCommand();
    }

    public static CreateTrainingCommand? GetNoCreateTrainingCommandData()
    {
        return null;
    }

}