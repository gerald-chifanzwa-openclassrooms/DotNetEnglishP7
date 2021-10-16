using System;
using FluentValidation;
using WebApi.Models;

namespace WebApi.Validators
{
    public class TradeModelValidator : AbstractValidator<TradeModel>
    {
        public TradeModelValidator()
        {
            RuleFor(x => x.Account).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.BuyQuantity).ValidateQuantity();
            RuleFor(x => x.BuyPrice).ValidateAmount();
            RuleFor(x => x.SellQuantity).ValidateQuantity();
            RuleFor(x => x.SellPrice).ValidateAmount();
            RuleFor(x => x.Benchmark).NotEmpty();
            RuleFor(x => x.TradeDate).NotEmpty();
            RuleFor(x => x.Security).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
            RuleFor(x => x.Trader).NotEmpty();
            RuleFor(x => x.Book).NotEmpty();
            RuleFor(x => x.CreationName).NotEmpty();
            RuleFor(x => x.CreationDate).NotEmpty();
            RuleFor(x => x.RevisionName).NotEmpty();
            RuleFor(x => x.DealName).NotEmpty();
            RuleFor(x => x.DealType).NotEmpty();
            RuleFor(x => x.SourceListId).NotEmpty();
            RuleFor(x => x.Side).NotEmpty();
        }
    }
}
