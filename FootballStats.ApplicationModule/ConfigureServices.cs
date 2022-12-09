using System.Reflection;
using FluentValidation;
using FootballStats.ApplicationModule.Coaches.Commands.CreateCoach;
using FootballStats.ApplicationModule.Coaches.Queries.GetAllCoaches;
using FootballStats.ApplicationModule.Coaches.Queries.GetCoachById;
using FootballStats.ApplicationModule.Coaches.Queries.GetFreeCoaches;
using FootballStats.ApplicationModule.Common.Behaviours;
using FootballStats.ApplicationModule.Common.Dtos.Coaches;
using FootballStats.ApplicationModule.Common.Wrappers;
using MediatR;
using MediatR.Pipeline;
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
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));

        services.AddScoped(typeof(IRequestExceptionHandler<GetCoachByIdQuery, Response<CoachReadDto>, Exception>),
             typeof(GetCoachByIdQueryExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<GetAllCoachesQuery, Response<CoachesListWithCountDto>, Exception>),
             typeof(GetAllCoachesQueryExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<GetFreeCoachesQuery, Response<CoachesListWithCountDto>, Exception>),
             typeof(GetFreeCoachesQueryExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<CreateCoachCommand, Response<CoachReadDto>, Exception>),
             typeof(CreateCoachCommandExceptionHandler));

        return services;
    }
}