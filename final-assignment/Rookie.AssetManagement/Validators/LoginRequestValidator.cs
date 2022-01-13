using FluentValidation;
using Rookie.AssetManagement.Business.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Validators
{
    public class LoginRequestValidator : BaseValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required").
                Matches(@"^[A-Za-z0-9]*$").WithMessage("Username must contain only numbers and characters");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password is at least 6 characters");
                //.Matches("^[^ “”]*$").WithMessage("Password must not contain “” or spaces.")
        }
    }
}
