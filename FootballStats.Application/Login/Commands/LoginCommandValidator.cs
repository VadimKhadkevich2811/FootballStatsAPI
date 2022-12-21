using FluentValidation;

namespace FootballStats.Application.Login.Commands;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(field => field.LoginId)
            .NotEmpty().WithMessage("Username/Email is required");

        RuleFor(field => field.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}