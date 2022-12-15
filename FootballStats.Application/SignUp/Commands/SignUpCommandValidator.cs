using FluentValidation;

namespace FootballStats.Application.SignUp.Commands;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(field => field.Username)
            .NotEmpty().WithMessage("Username is required");

        RuleFor(field => field.Password)
            .NotEmpty().WithMessage("Password is required");

        RuleFor(field => field.RepeatPassword)
            .NotEmpty().WithMessage("Repeating Password is required")
            .Equal(field => field.Password);

        RuleFor(field => field.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Valid email is required");
    }
}