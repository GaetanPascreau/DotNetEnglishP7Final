using FluentValidation;
using WebApi3.Domain.DTO;

namespace WebApi3.Validators
{
    public class RuleNameValidator : AbstractValidator<RuleNameDTO>
    {
        public RuleNameValidator()
        {
            RuleFor(rn => rn.Name).NotEmpty().WithMessage("Name is mandatory");
            RuleFor(rn => rn.Name).NotNull().WithMessage("Name is mandatory");
            RuleFor(rn => rn.Description).NotEmpty().WithMessage("Description is mandatory");
            RuleFor(rn => rn.Description).NotNull().WithMessage("Description is mandatory");
        }
    }
}
