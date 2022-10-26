using FootballStats.ApplicationModule.SignUp.Commands;

namespace FootballStats.UnitTests.MockData;

public class SignUpCommandMockData
{
    public static SignUpCommand GetEmptySignUpCommandData()
    {
        return new SignUpCommand();
    }

    public static SignUpCommand GetNoSignUpCommandData()
    {
        return null;
    }

}