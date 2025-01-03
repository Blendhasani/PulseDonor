using Microsoft.AspNetCore.Identity;
using PulseDonor.Infrastructure.Authentication.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int BloodTypeId { get; set; }

        public int GenderId { get; set; }
		public override string NormalizedUserName { get; set; }

		public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public override string? Email { get; set; }

        public override string? NormalizedEmail { get; set; }

        public string? ImagePath { get; set; }

        public DateTime? Birthdate { get; set; }

        public override string? PasswordHash { get; set; }

        public override string? SecurityStamp { get; set; }

        public override string? ConcurrencyStamp { get; set; }

        public override string? PhoneNumber { get; set; }

        public override bool EmailConfirmed { get; set; }

        public override bool PhoneNumberConfirmed { get; set; }

        public override bool TwoFactorEnabled { get; set; }

        public override DateTimeOffset? LockoutEnd { get; set; }

        public override bool LockoutEnabled { get; set; }

        public override int AccessFailedCount { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsActive { get; set; }

        public bool IsEligible { get; set; }

        public DateTime? LastDonationDate { get; set; }

        public DateTime? InsertedDate { get; set; }
        public virtual ICollection<ApplicationRole> Roles { get; set; }

    }
}
