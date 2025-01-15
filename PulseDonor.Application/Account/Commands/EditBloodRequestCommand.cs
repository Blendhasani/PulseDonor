using FluentValidation;
using PulseDonor.Application.Account.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Account.Commands
{
	public class EditAccountBloodRequestCommand
	{
		public int BloodTypeId { get; set; }
		public decimal Quantity { get; set; }
		public int UrgenceTypeId { get; set; }
		public int? HospitalId { get; set; }
		public DateOnly? DonationDate { get; set; }
		public TimeOnly? DonationTime { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public int? Age { get; set; }
	}

	public class EditBloodRequestCommandValidator : AbstractValidator<EditAccountBloodRequestCommand>
	{
		public EditBloodRequestCommandValidator()
		{
			RuleFor(x => x.BloodTypeId).NotNull().WithMessage("Kjo fushë është e obliguar!");
			RuleFor(x => x.Quantity).NotNull().WithMessage("Kjo fushë është e obliguar!");
			RuleFor(x => x.UrgenceTypeId).NotNull().WithMessage("Kjo fushë është e obliguar!");
		}
	}
}
