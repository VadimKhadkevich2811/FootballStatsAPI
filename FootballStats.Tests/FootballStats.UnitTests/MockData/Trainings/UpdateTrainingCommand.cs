using FootballStats.Application.Trainings.Commands.UpdateTraining;

namespace FootballStats.UnitTests.MockData.Trainings;

public class UpdateTrainingCommandMockData
{
    public static UpdateTrainingCommand GetEmptyUpdateTrainingCommandData()
    {
        return new UpdateTrainingCommand();
    }

}