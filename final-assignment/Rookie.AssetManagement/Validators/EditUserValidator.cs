using FluentValidation;
using Rookie.AssetManagement.Constants;
using Rookie.AssetManagement.Contracts.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rookie.AssetManagement.Validators
{
	/// <summary>
	/// Validator for CreateUserDto.
	/// <para></para>
	/// Don't need to create validators in controllers as 
	/// RegisterValidatorsFromAssembly(...) in Startup.cs 
	/// registers all validators derived from AbstractValidator within this assembly.
	/// </summary>
	public class EditUserValidator : AbstractValidator<EditUserDto>
	{
		public EditUserValidator()
		{
			RuleFor(eud => eud.DateOfBirth)
				.NotEmpty()
				.WithMessage("Date of birth is required")
				.LessThanOrEqualTo(eud => DateTime.Now.AddYears(-18))
				.WithMessage("User is under 18. Please select a different date");
			RuleFor(eud => eud.Gender)
				.NotEmpty()
				.WithMessage("Gender is required")
				.Must(gender => gender == Genders.Female || gender == Genders.Male)
				.WithMessage($"Must be either {Genders.Female} or {Genders.Male}");
			RuleFor(eud => eud.JoinedDate)
				.NotEmpty()
				.WithMessage("Joined date is required")
				.GreaterThanOrEqualTo(eud => eud.DateOfBirth.AddYears(18))
				.WithMessage("User under the age of 18 may not join company. Please select a different date")
				.Must(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
				.WithMessage("Joined date is Saturday or Sunday. Please select a different date");
			RuleFor(eud => eud.Type)
				.NotEmpty()
				.WithMessage("Type is required")
				.Must(type => type == Roles.Admin || type == Roles.Staff)
				.WithMessage($"Must be either {Roles.Admin} or {Roles.Staff}");
		}
	}
}
