using FootballStats.ApplicationModule.Coaches.Commands.CreateCoach;

namespace FootballStats.UnitTests.MockData.Coaches;

public class CreateCoachCommandMockData
{
    public static CreateCoachCommand GetEmptyCreateCoachCommandData()
    {
        return new CreateCoachCommand();
    }

    public static CreateCoachCommand GetNoCreateCoachCommandData()
    {
        return null;
    }

}