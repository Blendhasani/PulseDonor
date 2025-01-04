using FluentValidation;

namespace PulseDonor.MVC.Auth.Commands
{
	public class LoginCommand
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}

	public class LoginCommandValidator : AbstractValidator<LoginCommand>
	{
        public LoginCommandValidator()
        {
            RuleFor(x=>x.Email).NotEmpty();
            RuleFor(x=>x.Password).NotEmpty();
        }
    }
}
