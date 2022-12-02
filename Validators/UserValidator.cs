using FluentValidation;
using System.Linq;
using WebApi3.Domain.DTO;

namespace WebApi3.Validators
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("UserName is mandatory");
            RuleFor(u => u.UserName).NotNull().WithMessage("UserName is mandatory");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password is mandatory");
            RuleFor(u => u.Password).NotNull().WithMessage("Password is mandatory");
            RuleFor(u => u.Password).Must(password => SatisfySecurityRequirement(password)).WithMessage(
                "The password must satisfy the following requirements :" +
                " - Minimun length is 8 characters" +
                " - At least 1 lowercase character is required" +
                " - At least 1 upercase characcter is required" +
                " - At least 1 digit is required" +
                " - at least 1 non-alphanumeric character is required");
            RuleFor(u => u.FullName).NotEmpty().WithMessage("FullName is mandatory");
            RuleFor(u => u.FullName).NotNull().WithMessage("FullName is mandatory");
        }

        public bool SatisfySecurityRequirement(string password)
        {
            var passwordLength = password.Length;
            if(passwordLength < 8)
            {
                return false;
            }

            bool containsAtLeastOneLowercase = password.Any(char.IsLower);
            if (!containsAtLeastOneLowercase)
            {
                return false;
            }

            bool containsAtLeastOneUppercase = password.Any(char.IsUpper);
            if(!containsAtLeastOneUppercase)
            {
                return false;
            }

            bool containsAtLeastOneNonAlphaNumeric = password.Any(char.IsPunctuation);
            if (!containsAtLeastOneNonAlphaNumeric)
            {
                return false;
            }

            bool containsAtLeastOneDigit = password.Any(char.IsDigit);
            if (!containsAtLeastOneDigit)
            {
                return false;
            }

            return true;
        }
    }
}
