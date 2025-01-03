using FluentValidation;

namespace PulseDonor.MVC.User.Commands
{
	public class EditUserCommand
	{
		public string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		//public IFormFile ImagePath { get; set; }
	}

	public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
	{
		public EditUserCommandValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().WithMessage(Resource.RequiredField);
			RuleFor(x => x.LastName).NotEmpty().WithMessage(Resource.RequiredField);
			RuleFor(x => x.Email).NotEmpty().WithMessage(Resource.RequiredField);
		}
	}
}
