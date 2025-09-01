using Application.Features.Authentication.RefreshToken;

namespace Application.Features.Authentication.Validators;

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
