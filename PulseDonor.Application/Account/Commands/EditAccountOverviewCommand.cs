using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Application.Account.Commands
{
	public class EditAccountOverviewCommand
	{
		public string FirstName{ get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }	
		public int BloodTypeId { get; set; }
		public int PrimaryCityId { get; set; }
		public int? PrimaryCityIdToDelete { get; set; }

		public List<int>? SecondaryCities { get; set; }
		public List<int>? SecondaryCityIdsToDelete { get; set; }
		public bool Status { get;set; }
	}

	public class EditAccountOverviewCommandValidator : AbstractValidator<EditAccountOverviewCommand>
	{
        public EditAccountOverviewCommandValidator()
        {
			RuleFor(x => x.FirstName).NotEmpty().WithMessage("Kjo fushë është e obliguar!");
			RuleFor(x => x.LastName).NotEmpty().WithMessage("Kjo fushë është e obliguar!");
			RuleFor(x => x.Email).NotEmpty().WithMessage("Kjo fushë është e obliguar!");
			RuleFor(x => x.BloodTypeId).NotNull().WithMessage("Kjo fushë është e obliguar!");
			RuleFor(x => x.PrimaryCityId).NotNull().WithMessage("Kjo fushë është e obliguar!");
        }
    }


}
