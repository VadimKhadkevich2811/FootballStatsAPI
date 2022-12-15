using FootballStats.Application.Common.Interfaces;
using FootballStats.Application.Common.Options;
using Microsoft.Extensions.Options;

namespace FootballStats.Infrastructure.Services;

public class ValidationOptionsService : IValidationOptionsService
{
    private readonly ValidationOptions _validationOptions;
    public ValidationOptionsService(IOptions<ValidationOptions> validationOptions)
    {
        _validationOptions = validationOptions.Value;
    }

    public CoachValidationOptions GetCoachValidationOptions()
    {
        return _validationOptions.CoachValidation;
    }

    public PlayerValidationOptions GetPlayerValidationOptions()
    {
        return _validationOptions.PlayerValidation;
    }
    public TrainingValidationOptions GetTrainingValidationOptions()
    {
        return _validationOptions.TrainingValidation;
    }
}