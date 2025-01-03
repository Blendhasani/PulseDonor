using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.User.Commands
{
	public class EditUserAPICommand
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
	}


	public class EditUserAPICommandValidator : AbstractValidator<EditUserAPICommand>
	{
        public EditUserAPICommandValidator()
        {
			RuleFor(x => x.FirstName).NotEmpty().WithMessage("Required Field!");
			RuleFor(x => x.LastName).NotEmpty().WithMessage("Required Field!");
			RuleFor(x => x.Email).NotEmpty().WithMessage("Required Field!");
		}
    }
}
