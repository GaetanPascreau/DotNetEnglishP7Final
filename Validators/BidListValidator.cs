using FluentValidation;
using WebApi3.Domain.DTO;

namespace WebApi3.Validators
{
    public class BidListValidator : AbstractValidator<BidListDTO>
    {
        public BidListValidator()
        {
            RuleFor(b => b.Account).NotEmpty().WithMessage("Account is mandatory");
            RuleFor(b => b.Account).NotNull().WithMessage("Account is mandatory");
            RuleFor(b => b.Type).NotEmpty().WithMessage("Type is mandatory");
            RuleFor(b => b.Type).NotNull().WithMessage("Type is mandatory");
            RuleFor(b => b.BidQuantity).NotEmpty().WithMessage("Bid Quantity is mandatory");
            RuleFor(b => b.BidQuantity).NotNull().WithMessage("Bid Quantity is mandatory");
            RuleFor(b => b.BidQuantity).GetType().Equals(typeof(decimal));
        }
    }
}
