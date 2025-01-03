using FluentValidation;

namespace PulseDonor.MVC.User.Commands
{
	public class AddUserCommand
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		//public IFormFile? ImagePath { get; set; }
	}

	public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
	{
		public AddUserCommandValidator()
		{
			RuleFor(x => x.FirstName).NotEmpty().WithMessage(Resource.RequiredField);
			RuleFor(x => x.LastName).NotEmpty().WithMessage(Resource.RequiredField);
			RuleFor(x => x.Email).NotEmpty().WithMessage(Resource.RequiredField);
			RuleFor(x => x.Password).NotEmpty().WithMessage(Resource.RequiredField);
		}
	}

}
