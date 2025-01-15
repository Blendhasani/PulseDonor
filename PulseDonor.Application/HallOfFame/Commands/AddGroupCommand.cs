using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.HallOfFame.Commands
{
	public class AddGroupCommand
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int CityId { get; set; }

	}

	public class AddGroupCommandValidator : AbstractValidator<AddGroupCommand>
	{
		public AddGroupCommandValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Kjo fushë është e obliguar!");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Kjo fushë është e obliguar!");
			RuleFor(x => x.CityId).NotEmpty().WithMessage("Kjo fushë është e obliguar!");
		}
	}
}
