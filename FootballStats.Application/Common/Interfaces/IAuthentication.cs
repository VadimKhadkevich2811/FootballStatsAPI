namespace FootballStats.Application.Common.Interfaces;

public interface IAuthentication
{
    /// <summary>
    /// Asynchronously returns the authentication token.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.
    /// The task result contains a nullable <see cref="System.String"/> that represents authentication token.</returns>
    Task<string?> GetAuthenticationTokenAsync();
}