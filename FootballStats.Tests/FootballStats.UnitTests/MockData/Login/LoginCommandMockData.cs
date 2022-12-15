using FootballStats.ApplicationModule.Login.Commands;

namespace FootballStats.UnitTests.MockData.Login;

public class LoginCommandMockData
{
    public static LoginCommand GetEmptyLoginCommandData()
    {
        return new LoginCommand();
    }

    public static LoginCommand? GetNoLoginCommandData()
    {
        return null;
    }

}