using FluentValidation;
using WebApi3.Domain.DTO;

namespace WebApi3.Validators
{
    public class CurvePointValidator : AbstractValidator<CurvePointDTO>
    {
        public CurvePointValidator()
        {
            RuleFor(c => c.CurveId).NotEmpty().WithMessage("CurveId is mandatory.");
            RuleFor(c => c.CurveId).NotNull().WithMessage("CurveId is mandatory.");
            RuleFor(c => c.Term).NotEmpty().WithMessage("Term is mandatory.");
            RuleFor(c => c.CurveId).NotNull().WithMessage("Term is mandatory.");
            RuleFor(c => c.Value).NotEmpty().WithMessage("Value is mandatory.");
            RuleFor(c => c.CurveId).NotNull().WithMessage("Value is mandatory.");
            RuleFor(c => c.CurveId).GetType().Equals(typeof(int));
            RuleFor(c => c.Term).GetType().Equals(typeof(decimal));
            RuleFor(c => c.Value).GetType().Equals(typeof(decimal));
        }
    }
}
