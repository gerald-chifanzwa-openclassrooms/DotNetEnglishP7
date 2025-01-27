﻿using System.Text.Json;
using FluentValidation;
using Newtonsoft.Json.Schema;
using WebApi.Models;

namespace WebApi.Validators
{
    public class RuleNameModelValidator : AbstractValidator<RuleNameModel>
    {
        public RuleNameModelValidator()
        {
            // Validation rules for RuleNames
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Json).Cascade(CascadeMode.Stop).NotEmpty().Must(json =>
            {
                try
                {
                    // Try parse the json value, if it fails, validation should fail
                    JsonDocument.Parse(json);
                    return true;
                }
                catch (JsonException)
                {
                    return false;
                }
            }).WithMessage("{PropertyName} is not a valid JSON structure");
            RuleFor(x => x.SqlStr).NotEmpty();
            RuleFor(x => x.SqlPart).NotEmpty();
            RuleFor(x => x.Template).NotEmpty();
        }
    }
}
