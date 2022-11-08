using FluentValidation;
using OnlineLearningAppApi.Core.Mapper.Dtos;

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
