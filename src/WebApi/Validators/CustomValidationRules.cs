using System;
using FluentValidation;

namespace WebApi.Validators
{
    public static class CustomValidationRules
    {
        public static IRuleBuilderOptions<T, TProperty> ValidateAmount<T, TProperty>(this IRuleBuilder<T, TProperty> builder) where T : class where TProperty : IComparable<TProperty>, IComparable
        {
            return builder.GreaterThan(default(TProperty))
                .WithMessage("{PropertyName} must be a valid amount");
        }
        public static IRuleBuilderOptions<T, TProperty> ValidateQuantity<T, TProperty>(this IRuleBuilder<T, TProperty> builder) where T : class where TProperty : IComparable<TProperty>, IComparable
        {
            return builder.GreaterThan(default(TProperty))
                .WithMessage("{PropertyName} must be a valid quantity");
        }
    }
}
