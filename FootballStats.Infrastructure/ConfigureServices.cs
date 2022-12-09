using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.Domain.Entities;
using FootballStats.Infrastructure.Authentication;
using FootballStats.Infrastructure.Logging;
using FootballStats.Infrastructure.Persistence;
using FootballStats.Infrastructure.Persistence.Repositories;
using FootballStats.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Authentication:Domain"]
                    + $"{configuration["FootballStatsAuthentication:Domain"]}/";
                options.Audience = configuration["Authentication:Audience"]
                    + $"{configuration["FootballStatsAuthentication:Audience"]}/";
            });

        services.AddDbContext<FootballStatsDbContext>(opt => opt.UseSqlServer(
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

        return services;
    }
}