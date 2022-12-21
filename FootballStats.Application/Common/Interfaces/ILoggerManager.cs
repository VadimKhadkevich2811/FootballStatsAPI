namespace FootballStats.Application.Common.Interfaces;

public interface ILoggerManager
{
    /// <summary>
    /// Logs the diagnostic message at the Debug level.
    /// </summary>
    /// <param name="message">The <see cref="System.String"/> instance that represents message that should be logged.</param>
    /// <returns></returns>
    void LogDebug(string message);

    /// <summary>
    /// Logs the diagnostic message at the Error level.
    /// </summary>
    /// <param name="message">The <see cref="System.String"/> instance that represents message that should be logged.</param>
    /// <returns></returns>
    void LogError(string message);

    /// <summary>
    /// Logs the diagnostic message at the Info level.
    /// </summary>
    /// <param name="message">The <see cref="System.String"/> instance that represents message that should be logged.</param>
    /// <returns></returns>
    void LogInfo(string message);

    /// <summary>
    /// Logs the diagnostic message at the Warn level.
    /// </summary>
    /// <param name="message">The <see cref="System.String"/> instance that represents message that should be logged.</param>
    /// <returns></returns>
    void LogWarn(string message);
}