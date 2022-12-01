using FluentValidation;
using WebApi3.Domain;

namespace WebApi3.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("UserName is mandatory");
            RuleFor(u => u.UserName).NotNull().WithMessage("UserName is mandatory");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is mandatory");
            RuleFor(u => u.Password).NotNull().WithMessage("Password is mandatory");
            RuleFor(u => u.FullName).NotEmpty().WithMessage("FullName is mandatory");
            RuleFor(u => u.FullName).NotNull().WithMessage("FullName is mandatory");
        }
    }
}
