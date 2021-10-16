using FluentValidation;
using WebApi.Models;

namespace WebApi.Validators
{
    public class CurvePointModelValidator : AbstractValidator<CurvePointModel>
    {
        public CurvePointModelValidator()
        {
            RuleFor(c => c.CurveId).NotEmpty().GreaterThan(0);
            RuleFor(c => c.Term).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(c => c.CreationDate).NotEmpty();
            RuleFor(c => c.AsOfDate).NotEmpty();
            RuleFor(c => c.Value);
        }
    }
}
