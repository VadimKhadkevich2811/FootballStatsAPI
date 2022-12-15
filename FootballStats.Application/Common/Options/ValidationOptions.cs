namespace FootballStats.Application.Common.Options;

public class CoachValidationOptions
{
    public const string CoachValidation = "CoachValidation";
    public int MinAge { get; set; }
    public int MaxAge { get; set; }
}

public class PlayerValidationOptions
{
    public const string PlayerValidation = "PlayerValidation";
    public int MinAge { get; set; }
    public int MaxAge { get; set; }
}

public class TrainingValidationOptions
{
    public const string TrainingValidation = "TrainingValidation";
    public DateTime MinDate { get; set; }
}

public class ValidationOptions
{
    public const string Validation = "Validation";
    public CoachValidationOptions CoachValidation { get; set; } = default!;
    public PlayerValidationOptions PlayerValidation { get; set; } = default!;
    public TrainingValidationOptions TrainingValidation { get; set; } = default!;
}