using System.Reflection;
using FluentValidation;
using FootballStats.ApplicationModule.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FootballStats.ApplicationModule;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}