using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.User.Commands
{
	public class AddUserAPICommand
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		//public IFormFile ImagePath { get; set; }
	}

	//public class AddUserAPICommandValidator : AbstractValidator<AddUserAPICommand>
	//{
 //       public AddUserAPICommandValidator()
 //       {
	//		RuleFor(x => x.FirstName).NotEmpty().WithMessage("Required Field!");
	//		RuleFor(x => x.LastName).NotEmpty().WithMessage("Required Field!");
	//		RuleFor(x => x.Email).NotEmpty().WithMessage("Required Field!");
	//		RuleFor(x => x.Password).NotEmpty().WithMessage("Required Field!");
	//	}
 //   }
}
