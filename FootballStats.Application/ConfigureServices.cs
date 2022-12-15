using System.Reflection;
using FluentValidation;
using FootballStats.Application.Coaches.Commands.CreateCoach;
using FootballStats.Application.Coaches.Commands.DeleteCoach;
using FootballStats.Application.Coaches.Commands.UpdateCoach;
using FootballStats.Application.Coaches.Commands.UpdateCoachDetail;
using FootballStats.Application.Coaches.Dtos;
using FootballStats.Application.Coaches.Queries.GetAllCoaches;
using FootballStats.Application.Coaches.Queries.GetCoachById;
using FootballStats.Application.Coaches.Queries.GetFreeCoaches;
using FootballStats.Application.Common.Behaviours;
using FootballStats.Application.Common.Options;
using FootballStats.Application.Common.Wrappers;
using FootballStats.Application.Login.Commands;
using FootballStats.Application.Login.Dtos;
using FootballStats.Application.Players.Commands.CreatePlayer;
using FootballStats.Application.Players.Commands.DeletePlayer;
using FootballStats.Application.Players.Commands.UpdatePlayer;
using FootballStats.Application.Players.Commands.UpdatePlayerDetail;
using FootballStats.Application.Players.Dtos;
using FootballStats.Application.Players.Queries.GetAllPlayers;
using FootballStats.Application.Players.Queries.GetFreePlayers;
using FootballStats.Application.Players.Queries.GetPlayerById;
using FootballStats.Application.SignUp.Commands;
using FootballStats.Application.SignUp.Dtos;
using FootballStats.Application.Trainings.Commands.CreateTraining;
using FootballStats.Application.Trainings.Commands.DeleteTraining;
using FootballStats.Application.Trainings.Commands.UpdateTraining;
using FootballStats.Application.Trainings.Commands.UpdateTrainingDetail;
using FootballStats.Application.Trainings.Dtos;
using FootballStats.Application.Trainings.Queries.GetAllTrainings;
using FootballStats.Application.Trainings.Queries.GetTrainingById;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootballStats.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ValidationOptions>(configuration.GetSection("Validation"));
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
        services.AddScoped(typeof(IRequestExceptionHandler<DeleteCoachCommand, Response<bool>, Exception>),
           typeof(DeleteCoachCommandExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<UpdateCoachCommand, Response<bool>, Exception>),
           typeof(UpdateCoachCommandExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<UpdateCoachDetailCommand, Response<bool>, Exception>),
           typeof(UpdateCoachDetailCommandExceptionHandler));

        services.AddScoped(typeof(IRequestExceptionHandler<GetPlayerByIdQuery, Response<PlayerReadDto>, Exception>),
           typeof(GetPlayerByIdQueryExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<GetAllPlayersQuery, Response<PlayersListWithCountDto>, Exception>),
           typeof(GetAllPlayersQueryExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<GetFreePlayersQuery, Response<PlayersListWithCountDto>, Exception>),
           typeof(GetFreePlayersQueryExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<CreatePlayerCommand, Response<PlayerReadDto>, Exception>),
           typeof(CreatePlayerCommandExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<DeletePlayerCommand, Response<bool>, Exception>),
           typeof(DeletePlayerCommandExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<UpdatePlayerCommand, Response<bool>, Exception>),
           typeof(UpdatePlayerCommandExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<UpdatePlayerDetailCommand, Response<bool>, Exception>),
           typeof(UpdatePlayerDetailCommandExceptionHandler));

        services.AddScoped(typeof(IRequestExceptionHandler<GetTrainingByIdQuery, Response<TrainingReadDto>, Exception>),
           typeof(GetTrainingByIdQueryExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<GetAllTrainingsQuery, Response<TrainingsListWithCountDto>, Exception>),
           typeof(GetAllTrainingsQueryExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<CreateTrainingCommand, Response<TrainingReadDto>, Exception>),
           typeof(CreateTrainingCommandExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<DeleteTrainingCommand, Response<bool>, Exception>),
           typeof(DeleteTrainingCommandExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<UpdateTrainingCommand, Response<bool>, Exception>),
           typeof(UpdateTrainingCommandExceptionHandler));
        services.AddScoped(typeof(IRequestExceptionHandler<UpdateTrainingDetailCommand, Response<bool>, Exception>),
           typeof(UpdateTrainingDetailCommandExceptionHandler));

        services.AddScoped(typeof(IRequestExceptionHandler<LoginCommand, Response<LoginDto>, Exception>),
           typeof(LoginCommandExceptionHandler));

        services.AddScoped(typeof(IRequestExceptionHandler<SignUpCommand, Response<SignUpDto?>, Exception>),
           typeof(SignUpCommandExceptionHandler));

        return services;
    }
}