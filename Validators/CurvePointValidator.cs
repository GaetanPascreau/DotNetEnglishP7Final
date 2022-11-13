using FluentValidation;
using WebApi3.Domain.DTO;

namespace WebApi3.Validators
{
    public class CurvePointValidator : AbstractValidator<CurvePointDTO>
    {
        public CurvePointValidator()
        {
            RuleFor(c => c.CurveId).NotEmpty();
            RuleFor(c => c.Term).NotEmpty();
            RuleFor(c => c.Value).NotEmpty();
            RuleFor(c => c.CurveId).GetType().Equals(typeof(int));
            RuleFor(c => c.Term).GetType().Equals(typeof(decimal));
            RuleFor(c => c.Value).GetType().Equals(typeof(decimal));
        }
    }
}
