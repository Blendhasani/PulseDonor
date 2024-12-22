using FluentValidation;

namespace PulseDonor.MVC.City.Commands
{
	public class EditCityCommand
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class EditCityCommandValidator : AbstractValidator<EditCityCommand>
	{
		public EditCityCommandValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage(Resource.RequiredField);
		}
	}
}
