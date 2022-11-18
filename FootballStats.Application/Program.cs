using FootballStats.ApplicationModule;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.Infrastructure.Authentication;
using FootballStats.Infrastructure.Logging;
using FootballStats.Infrastructure.Persistence;
using FootballStats.Infrastructure.Persistence.Repositories;
using FootballStats.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

LoggerManagerExtensions.LoadConfiguration();
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor.HttpContext.Request;
    var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
    return new UriService(uri);
});

// Add services to the container.
builder.Services.AddScoped<IApplicationDbContext, FootballStatsDbContext>();
builder.Services.AddScoped<IAuthentication, ApplicationAuthentication>();
builder.Services.AddScoped<ISignUpRepository, SignUpRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IPlayersRepository, PlayersRepository>();
builder.Services.AddScoped<ITrainingsRepository, TrainingsRepository>();
builder.Services.AddScoped<ICoachesRepository, CoachesRepository>();


builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Authentication:Domain"]
            + $"{builder.Configuration["FootballStatsAuthentication:Domain"]}/";
        options.Audience = builder.Configuration["Authentication:Audience"]
            + $"{builder.Configuration["FootballStatsAuthentication:Audience"]}/";
    });
string str = builder.Configuration.GetConnectionString("FootballStatsDBConnection") +
        $"{builder.Configuration["FootballStatsDBConnection:Password"]};";
builder.Services.AddDbContext<FootballStatsDbContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("FootballStatsDBConnection") +
        $"{builder.Configuration["FootballStatsDBConnection:Password"]};",
            options => options.EnableRetryOnFailure()
));

builder.Services.ConfigureLoggerService();

builder.Services.AddControllers().AddNewtonsoftJson(services =>
        services.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

builder.Services.AddApplicationServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();

NLog.LogManager.Shutdown();