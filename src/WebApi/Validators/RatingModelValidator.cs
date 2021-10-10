using FluentValidation;
using WebApi.Models;

namespace WebApi.Validators
{
    public class RatingModelValidator : AbstractValidator<RatingModel>
    {
        public RatingModelValidator()
        {
            RuleFor(r => r.SandPRating).NotEmpty();
            RuleFor(r => r.MoodysRating).NotEmpty();
            RuleFor(r => r.FitchRating).NotEmpty();
            RuleFor(r => r.OrderNumber).NotEmpty().GreaterThan(0);
        }
    }
}
