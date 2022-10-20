using FluentValidation;

namespace FootballStats.ApplicationModule.Login.Commands;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(field => field.LoginId)
            .NotEmpty();

        RuleFor(field => field.Password)
            .NotEmpty();
    }
}