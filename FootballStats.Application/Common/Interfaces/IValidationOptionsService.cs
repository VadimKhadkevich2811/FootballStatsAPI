using FootballStats.Application.Common.Options;

namespace FootballStats.Application.Common.Interfaces;

public interface IValidationOptionsService
{
    /// <summary>
    /// Returns the validation options for Coach instance.
    /// </summary>
    /// <returns>An instance of the <see cref="CoachValidationOptions"/> class representing the validation options for <see cref="Coach"/> instance.</returns>
    CoachValidationOptions GetCoachValidationOptions();

    /// <summary>
    /// Returns the validation options for Player instance.
    /// </summary>
    /// <returns>An instance of the <see cref="PlayerValidationOptions"/> class representing the validation options for <see cref="Player"/> instance.</returns>
    PlayerValidationOptions GetPlayerValidationOptions();

    /// <summary>
    /// Returns the validation options for Training instance.
    /// </summary>
    /// <returns>An instance of the <see cref="TrainingValidationOptions"/> class representing the validation options for <see cref="Training"/> instance.</returns>
    TrainingValidationOptions GetTrainingValidationOptions();
}