using FluentValidation;
using OnlineLearningAppApi.Models.ApiModels;

namespace OnlineLearningAppApi.Validators
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenResource>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
