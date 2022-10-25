using FootballStats.ApplicationModule;
using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.Infrastructure.Authentication;
using FootballStats.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IApplicationDbContext, FootballStatsDbContext>();
builder.Services.AddScoped<IAuthentication, ApplicationAuthentication>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Auth0:Domain"]
            + $"{builder.Configuration["FootballStatsAuthentication:Domain"]}/";
        options.Audience = builder.Configuration["Auth0:Audience"]
            + $"{builder.Configuration["FootballStatsAuthentication:Audience"]}/";
    });
string str = builder.Configuration.GetConnectionString("FootballStatsDBConnection") +
        $"{builder.Configuration["FootballStatsDBConnection:Password"]};";
builder.Services.AddDbContext<FootballStatsDbContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("FootballStatsDBConnection") +
        $"{builder.Configuration["FootballStatsDBConnection:Password"]};",
            options => options.EnableRetryOnFailure()
));

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

app.MapControllers();

app.Run();
