using FluentValidation;
using OnlineLearningAppApi.Models;

namespace OnlineLearningAppApi.Services.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.ClientId).NotEmpty();
        }
    }
}
