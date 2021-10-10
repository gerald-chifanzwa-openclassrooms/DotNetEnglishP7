using FluentValidation;
using WebApi.Models;

namespace WebApi.Validators
{
    public class BidListModelValidator: AbstractValidator<BidListModel>
    {
        public BidListModelValidator()
        {
            RuleFor(x => x.Account).NotEmpty();
            RuleFor(x => x.AskAmount).NotEmpty().ValidateAmount();
            RuleFor(x => x.AskQuantity).NotEmpty().ValidateQuantity();
            RuleFor(x => x.Benchmark).NotEmpty();
            RuleFor(x => x.BidAmount).NotEmpty().ValidateAmount();
            RuleFor(x => x.BidQuantity).NotEmpty().ValidateQuantity();
            RuleFor(x => x.Book).NotEmpty();
            RuleFor(x => x.Commentary).NotEmpty();
            RuleFor(x => x.CreationDate).NotEmpty();
            RuleFor(x => x.CreationName).NotEmpty();
            RuleFor(x => x.DealName).NotEmpty();
            RuleFor(x => x.DealType).NotEmpty();
            RuleFor(x => x.ListDate).NotEmpty();
            RuleFor(x => x.RevisionDate).NotEmpty();
            RuleFor(x => x.RevisionName).NotEmpty();
            RuleFor(x => x.Security).NotEmpty();
            RuleFor(x => x.Side).NotEmpty();
            RuleFor(x => x.SourceListId).NotEmpty();
            RuleFor(x => x.Status).NotEmpty();
            RuleFor(x => x.Trader).NotEmpty();
            RuleFor(x => x.Type).NotEmpty();
        }
    }
}
