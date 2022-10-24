namespace FootballStats.ApplicationModule.Common.Interfaces;

public interface IAuthentication
{
    Task<string> GetAuthenticationToken();
}