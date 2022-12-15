using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Interfaces.Repositories;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Players.Dtos;
using FootballStats.Application.Trainings.Dtos;
using FootballStats.Domain.Entities;
using FootballStats.Infrastructure.Authentication;
using FootballStats.Infrastructure.Logging;
using FootballStats.Infrastructure.Persistence;
using FootballStats.Infrastructure.Persistence.Repositories;
using FootballStats.Infrastructure.Services;
using FootballStats.Infrastructure.Services.ResponseWrappers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FootballStats.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUriService>(o =>
        {
            var accessor = o.GetRequiredService<IHttpContextAccessor>();
            var request = accessor.HttpContext?.Request;
            var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
            return new UriService(uri);
        });

        services.ConfigureLoggerService();

        services.AddAuthentication()
            .AddCookie("auth", options =>
            {
                options.Cookie.Name = "userCookie";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = new TimeSpan(1, 0, 0);
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = redirectContext =>
                    {
                        redirectContext.HttpContext.Response.StatusCode = 401;
                        redirectContext.HttpContext.Response.ContentType = "application/json";
                        redirectContext.HttpContext.Response.WriteAsJsonAsync(
                            new ResponseProblemDetails("Unauthorized",
                                "User is not authorized",
                                401, redirectContext.HttpContext.Request.Path.Value));
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddDbContext<FootballStatsDbContext>(opt => opt
            .UseLazyLoadingProxies()
            .UseSqlServer(
                configuration.GetConnectionString("FootballStatsDBConnection") +
                    $"{configuration["FootballStatsDBConnection:Password"]};",
                        options => options.EnableRetryOnFailure()
        ));

        services.AddScoped<IApplicationDbContext, FootballStatsDbContext>();
        services.AddScoped<IAuthentication, ApplicationAuthentication>();
        services.AddScoped<ISignUpRepository, SignUpRepository>();
        services.AddScoped<ILoginRepository, LoginRepository>();
        services.AddScoped<IPlayersRepository, PlayersRepository>();
        services.AddScoped<ITrainingsRepository, TrainingsRepository>();
        services.AddScoped<ICoachesRepository, CoachesRepository>();

        services.AddScoped<ISortHelper<Player>, SortHelper<Player>>();
        services.AddScoped<ISortHelper<Training>, SortHelper<Training>>();
        services.AddScoped<ISortHelper<Coach>, SortHelper<Coach>>();

        services.AddTransient<IValidationOptionsService, ValidationOptionsService>();

        services.AddScoped<IResponseWrapper<PlayerReadDto, PlayersListWithCountDto>, PlayerResponseWrapper>();
        services.AddScoped<IResponseWrapper<TrainingReadDto, TrainingsListWithCountDto>, TrainingResponseWrapper>();
        services.AddScoped<IResponseWrapper<CoachReadDto, CoachesListWithCountDto>, CoachResponseWrapper>();

        return services;
    }
}