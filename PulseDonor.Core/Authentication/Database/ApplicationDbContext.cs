using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PulseDonor.Infrastructure.Authentication.Database.Models;
using PulseDonor.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseDonor.Infrastructure.Authentication.Database
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ApplicationUser>(entity =>
			{
				entity.ToTable("Users"); // Map to "User" table

				entity.HasMany(d => d.Roles)
					.WithMany(p => p.Users)
					.UsingEntity<Dictionary<string, object>>(
						"UserRole", // Custom table name
						l => l.HasOne<ApplicationRole>().WithMany().HasForeignKey("RoleId"),
						r => r.HasOne<ApplicationUser>().WithMany().HasForeignKey("UserId"),
						j =>
						{
							j.HasKey("UserId", "RoleId");

							j.ToTable("UserRoles", "dbo");

							j.IndexerProperty<string>("UserId").HasMaxLength(128);
						});
				//entity.Ignore(e => e.EmailConfirmed);
				//entity.Ignore(e => e.PhoneNumberConfirmed);
			});

			modelBuilder.Entity<ApplicationRole>(entity =>
			{
				entity.ToTable("Roles");

				entity.Property(e => e.Name).HasMaxLength(256);
				entity.Property(e => e.NormalizedName).HasMaxLength(256);
			});

			modelBuilder.Entity<IdentityUserRole<string>>()
				.ToTable("UserRoles");
		}
	}
}
