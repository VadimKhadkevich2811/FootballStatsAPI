using FluentValidation;
using FootballStats.ApplicationModule;
using FootballStats.ApplicationModule.Common.Behaviours;
using FootballStats.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IApplicationDbContext, FootballStatsDbContext>(); 
string str = builder.Configuration.GetConnectionString("FootballStatsDBConnection") +
        $"{builder.Configuration["FootballStatsDBConnection:Password"]};";
builder.Services.AddDbContext<FootballStatsDbContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("FootballStatsDBConnection") +
        $"{builder.Configuration["FootballStatsDBConnection:Password"]};",
            options => options.EnableRetryOnFailure()
));
//builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<FootballStatsDbContext>());

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
