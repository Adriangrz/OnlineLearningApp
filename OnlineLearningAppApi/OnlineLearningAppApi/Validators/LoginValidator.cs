using FluentValidation;
using OnlineLearningAppApi.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Services.Validators
{
    public class LoginValidator : AbstractValidator<LoginResource>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
