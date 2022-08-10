using FluentValidation;
using OnlineLearningAppApi.Models.ApiModels;

namespace OnlineLearningAppApi.Validators
{
    public class RegistrationValidator : AbstractValidator<RegistrationResource>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().Matches(@"^\p{L}+$");
            RuleFor(x => x.LastName).NotEmpty().Matches(@"^\p{L}+$");
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).Matches("(?=.*[0-9])(?=.*[^A-Za-z0-9])(?=.*[A-Za-z])");
            RuleFor(x => x.SiteRules).Equal(true);
        }
    }
}
