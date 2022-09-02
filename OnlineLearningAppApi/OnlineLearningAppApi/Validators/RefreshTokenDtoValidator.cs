using FluentValidation;
using OnlineLearningAppApi.Models;

namespace OnlineLearningAppApi.Validators
{
    public class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenDtoValidator()
        {
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
