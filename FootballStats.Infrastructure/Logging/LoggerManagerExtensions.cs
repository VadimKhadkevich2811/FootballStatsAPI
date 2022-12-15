using FootballStats.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace FootballStats.Infrastructure.Logging;

public static class LoggerManagerExtensions
{
    public static void LoadConfiguration()
    {
        LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
    }

    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }
}