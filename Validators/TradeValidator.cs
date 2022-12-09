using FluentValidation;
using WebApi3.Domain.DTO;

namespace WebApi3.Validators
{
    public class TradeValidator : AbstractValidator<TradeDTO>
    {
        public TradeValidator()
        {
            //This can be a buy OR a sell, so the quantity/price fields should accept Null or empty, but only if the corresponding type wasn't selected
            RuleFor(t => t.Account).NotEmpty().WithMessage("Account is mandatory");
            RuleFor(t => t.Account).NotNull().WithMessage("Account is mandatory");
            RuleFor(t => t.Type).NotEmpty().WithMessage("Type is mandatory");
            RuleFor(t => t.Type).NotNull().WithMessage("Type is mandatory");

            RuleFor(t => t.BuyQuantity).GetType().Equals(typeof(decimal));
            RuleFor(t => t.BuyQuantity).NotEmpty().WithMessage("You are buying. BuyQuantity is mandatory").When(t => t.Type == "buy");
            RuleFor(t => t.BuyQuantity).NotNull().WithMessage("You are buying. BuyQuantity is mandatory").When(t => t.Type == "buy");
            RuleFor(t => t.BuyQuantity).GreaterThan(0).WithMessage("You are buying. BuyQuantity is mandatory").When(t => t.Type == "buy");
            RuleFor(t => t.BuyQuantity).Null().WithMessage("You are selling. BuyQuantity must be null.").When(t => t.Type == "sell");

            RuleFor(t => t.BuyPrice).GetType().Equals(typeof(decimal));
            RuleFor(t => t.BuyPrice).NotEmpty().WithMessage("You are buying. BuyPrice is mandatory").When(t => t.Type == "buy");
            RuleFor(t => t.BuyPrice).NotNull().WithMessage("You are buying. BuyPrice is mandatory").When(t => t.Type == "buy");
            RuleFor(t => t.BuyPrice).GreaterThan(0).WithMessage("You are buying. BuyPrice is mandatory").When(t => t.Type == "buy");
            RuleFor(t => t.BuyPrice).Null().WithMessage("You are selling. BuyPrice must be null.").When(t => t.Type == "sell");

            RuleFor(t => t.SellQuantity).GetType().Equals(typeof(decimal));
            RuleFor(t => t.SellQuantity).NotEmpty().WithMessage("You are selling. SellQuantity is mandatory").When(t => t.Type == "sell");
            RuleFor(t => t.SellQuantity).NotNull().WithMessage("You are selling. SellQuantity is mandatory").When(t => t.Type == "sell");
            RuleFor(t => t.SellQuantity).GreaterThan(0).WithMessage("You are selling. SellQuantity is mandatory").When(t => t.Type == "sell");
            RuleFor(t => t.SellQuantity).Null().WithMessage("You are buying. SellQuantity must be null.").When(t => t.Type == "buy");

            RuleFor(t => t.SellPrice).GetType().Equals(typeof(decimal));
            RuleFor(t => t.SellPrice).NotEmpty().WithMessage("You are selling. SellPrice is mandatory").When(t => t.Type == "sell");
            RuleFor(t => t.SellPrice).NotNull().WithMessage("You are selling. SellPrice is mandatory").When(t => t.Type == "sell");
            RuleFor(t => t.SellPrice).GreaterThan(0).WithMessage("You are selling. SellPrice is mandatory").When(t => t.Type == "sell");
            RuleFor(t => t.SellPrice).Null().WithMessage("You are buying. SellPrice must be null.").When(t => t.Type == "buy");
        }
    }
}
