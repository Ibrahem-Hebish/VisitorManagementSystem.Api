using Application.Authentication.RefreshToken;

namespace Application.Authentication.Validators;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidator()
    {
        //RuleFor(x => x.UserTokenId)
        //    .NotEmpty()
        //    .WithMessage("Id is required.");

        //RuleFor(x => x.UserId)
        //    .NotEmpty()
        //    .WithMessage("UserId is required.");

    }
}
