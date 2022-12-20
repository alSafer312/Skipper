using FluentValidation;
using Skipper.Models.DTOs.Incomig;

namespace Skipper.Validators
{
    public class AuthenticateRequestValidator: AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(ar => ar.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Invalid Email addres");
            RuleFor(ar => ar.Password).NotNull().NotEmpty().MinimumLength(8).WithMessage("Invalid password");
        }
    }
}
