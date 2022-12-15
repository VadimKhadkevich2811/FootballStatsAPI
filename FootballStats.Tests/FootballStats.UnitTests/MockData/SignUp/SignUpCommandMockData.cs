using FootballStats.Application.SignUp.Commands;

namespace FootballStats.UnitTests.MockData.SignUp;

public class SignUpCommandMockData
{
    public static SignUpCommand GetEmptySignUpCommandData()
    {
        return new SignUpCommand();
    }

    public static SignUpCommand? GetNoSignUpCommandData()
    {
        return null;
    }

}