using FootballStats.ApplicationModule;
using FootballStats.Infrastructure;
using FootballStats.Infrastructure.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

LoggerManagerExtensions.LoadConfiguration();
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

builder.Services.AddHttpContextAccessor();

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(services =>
        services.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

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