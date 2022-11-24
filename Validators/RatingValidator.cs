using FluentValidation;
using WebApi3.Domain;

namespace WebApi3.Validators
{
    public class RatingValidator : AbstractValidator<Rating>
    {
        public RatingValidator()
        {
            RuleFor(r => r.MoodysRating).NotEmpty().WithMessage("Moodys' rating is mandatory");
            RuleFor(r => r.MoodysRating).NotNull().WithMessage("Moodys' rating is mandatory");
            RuleFor(r => r.SandPRating).NotEmpty().WithMessage("Sand's rating is mandatory");
            RuleFor(r => r.SandPRating).NotNull().WithMessage("Sand's rating is mandatory");
            RuleFor(r => r.FitchRating).NotEmpty().WithMessage("Fitch's rating is mandatory");
            RuleFor(r => r.FitchRating).NotNull().WithMessage("Fitch's rating is mandatory");
            RuleFor(r => r.OrderNumber).NotEmpty().WithMessage("Order number is mandatory");
            RuleFor(r => r.OrderNumber).NotNull().WithMessage("Order number is mandatory");
            RuleFor(r => r.OrderNumber).GetType().Equals(typeof(int));
        }
    }
}
