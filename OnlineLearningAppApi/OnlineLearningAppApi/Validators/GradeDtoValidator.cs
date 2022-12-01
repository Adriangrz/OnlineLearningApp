using Core.Mapper.Dtos;
using FluentValidation;
using OnlineLearningAppApi.Core.Mapper.Dtos;

namespace OnlineLearningAppApi.Validators
{
    public class GradeDtoValidator : AbstractValidator<GradeDto>
    {
        public GradeDtoValidator()
        {
            RuleFor(x => x.Grade).NotEmpty().LessThanOrEqualTo(5).GreaterThanOrEqualTo(1);
        }
    }
}
