using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PulseDonor.Infrastructure.Models;

public partial class DevPulsedonorContext : DbContext
{
    public DevPulsedonorContext()
    {
    }

    public DevPulsedonorContext(DbContextOptions<DevPulsedonorContext> options)
        : base(options)
    {
    }

    public override int SaveChanges()
    {
        SetDefaultValuesForEntities();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetDefaultValuesForEntities();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void SetDefaultValuesForEntities()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added && e.Entity != null);

        foreach (var entry in entries)
        {
            SetDefaultValues(entry);
        }
    }

    private void SetDefaultValues(EntityEntry entry)
    {
        var entity = entry.Entity;
        var entityType = entity.GetType();

        var insertedFromProperty = entityType.GetProperty("InsertedFrom");
        var insertedDateProperty = entityType.GetProperty("InsertedDate");

        if (insertedFromProperty != null && insertedFromProperty.PropertyType == typeof(string))
        {
            var currentValue = insertedFromProperty.GetValue(entity) as string;
            if (string.IsNullOrEmpty(currentValue))
            {
                insertedFromProperty.SetValue(entity, "ADMIN");
            }
        }

        if (insertedDateProperty != null && insertedDateProperty.PropertyType == typeof(DateTime?))
        {
            var currentValue = insertedDateProperty.GetValue(entity) as DateTime?;
            if (currentValue == null)
            {
                insertedDateProperty.SetValue(entity, DateTime.UtcNow);
            }
        }
    }


    public virtual DbSet<BloodDonationPoint> BloodDonationPoints { get; set; }

    public virtual DbSet<BloodRequest> BloodRequests { get; set; }

    public virtual DbSet<BloodRequestApplication> BloodRequestApplications { get; set; }

    public virtual DbSet<BloodType> BloodTypes { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupMember> GroupMembers { get; set; }

    public virtual DbSet<GroupMemberJoinCode> GroupMemberJoinCodes { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<NotificationUser> NotificationUsers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleClaim> RoleClaims { get; set; }

    public virtual DbSet<SuccessStory> SuccessStories { get; set; }

    public virtual DbSet<SuccessStoryFile> SuccessStoryFiles { get; set; }

    public virtual DbSet<UrgenceType> UrgenceTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCity> UserCities { get; set; }

    public virtual DbSet<UserClaim> UserClaims { get; set; }

    public virtual DbSet<UserLogin> UserLogins { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    => optionsBuilder.UseSqlServer("Server=localhost;Database=dev-pulsedonor;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True");
    //=> optionsBuilder.UseSqlServer("Server=SQL6029.site4now.net;Database=db_ab1c2b_pulsedonor;User Id=db_ab1c2b_pulsedonor_admin;Password=yd8V_uXVXbYCJ!R;Encrypt=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<BloodDonationPoint>(entity =>
        {
            entity.ToTable("BloodDonationPoint");

            entity.Property(e => e.Address).HasMaxLength(256);
            entity.Property(e => e.InsertedDate).HasColumnType("datetime");
            entity.Property(e => e.InsertedFrom).HasMaxLength(128);
            entity.Property(e => e.Latitude).HasMaxLength(50);
            entity.Property(e => e.Longitude).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedFrom).HasMaxLength(128);
        });

        modelBuilder.Entity<BloodRequest>(entity =>
        {
            entity.ToTable("BloodRequest");

            entity.HasIndex(e => e.AuthorId, "IX_BloodRequest_AuthorId");

            entity.HasIndex(e => e.BloodTypeId, "IX_BloodRequest_BloodTypeId");

            entity.HasIndex(e => e.DonorId, "IX_BloodRequest_DonorId");

            entity.HasIndex(e => e.HospitalId, "IX_BloodRequest_HospitalId");

            entity.HasIndex(e => e.UrgenceTypeId, "IX_BloodRequest_UrgenceTypeId");

            entity.Property(e => e.AuthorId).HasMaxLength(128);
            entity.Property(e => e.DonorId).HasMaxLength(128);
            entity.Property(e => e.FirstName).HasMaxLength(64);
            entity.Property(e => e.InsertedDate).HasColumnType("datetime");
            entity.Property(e => e.InsertedFrom).HasMaxLength(128);
            entity.Property(e => e.LastName).HasMaxLength(64);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedFrom).HasMaxLength(128);
            entity.Property(e => e.PostKey).HasMaxLength(10);
            entity.Property(e => e.Quantity).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Author).WithMany(p => p.BloodRequestAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloodRequest_AspNetUsers");

            entity.HasOne(d => d.BloodType).WithMany(p => p.BloodRequests)
                .HasForeignKey(d => d.BloodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloodRequest_BloodType");

            entity.HasOne(d => d.Donor).WithMany(p => p.BloodRequestDonors)
                .HasForeignKey(d => d.DonorId)
                .HasConstraintName("FK_BloodRequest_AspNetUsers1");

            entity.HasOne(d => d.Hospital).WithMany(p => p.BloodRequests)
                .HasForeignKey(d => d.HospitalId)
                .HasConstraintName("FK_BloodRequest_Hospital");

            entity.HasOne(d => d.UrgenceType).WithMany(p => p.BloodRequests)
                .HasForeignKey(d => d.UrgenceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloodRequest_UrgenceType");
        });

        modelBuilder.Entity<BloodRequestApplication>(entity =>
        {
            entity.ToTable("BloodRequestApplication");

            entity.HasIndex(e => e.BloodRequestId, "IX_BloodRequestApplication_BloodRequestId");

            entity.HasIndex(e => e.UserId, "IX_BloodRequestApplication_UserId");

            entity.Property(e => e.InsertedDate).HasColumnType("datetime");
            entity.Property(e => e.InsertedFrom).HasMaxLength(128);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedFrom).HasMaxLength(128);
            entity.Property(e => e.UserId).HasMaxLength(128);

            entity.HasOne(d => d.BloodRequest).WithMany(p => p.BloodRequestApplications)
                .HasForeignKey(d => d.BloodRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloodRequestApplication_BloodRequest");

            entity.HasOne(d => d.User).WithMany(p => p.BloodRequestApplications)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BloodRequestApplication_AspNetUsers");
        });

        modelBuilder.Entity<BloodType>(entity =>
        {
            entity.ToTable("BloodType");

            entity.Property(e => e.Type).HasMaxLength(10);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.Name).HasMaxLength(10);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("Gender");

            entity.Property(e => e.Type).HasMaxLength(12);
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("Group");

            entity.HasIndex(e => e.CityId, "IX_Group_CityId");

            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.InsertedDate).HasColumnType("datetime");
            entity.Property(e => e.InsertedFrom).HasMaxLength(128);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedFrom).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(64);

            entity.HasOne(d => d.City).WithMany(p => p.Groups)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Group_City");
        });

        modelBuilder.Entity<GroupMember>(entity =>
        {
            entity.ToTable("GroupMember");

            entity.HasIndex(e => e.GroupId, "IX_GroupMember_GroupId");

            entity.HasIndex(e => e.MemberId, "IX_GroupMember_MemberId");

            entity.Property(e => e.InsertedDate).HasColumnType("datetime");
            entity.Property(e => e.InsertedFrom).HasMaxLength(128);
            entity.Property(e => e.MemberId).HasMaxLength(128);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedFrom).HasMaxLength(128);

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupMember_Group");

            entity.HasOne(d => d.Member).WithMany(p => p.GroupMembers)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupMember_AspNetUsers");
        });

        modelBuilder.Entity<GroupMemberJoinCode>(entity =>
        {
            entity.ToTable("GroupMemberJoinCode");

            entity.HasIndex(e => e.GroupId, "IX_GroupMemberJoinCode_GroupId");

            entity.HasIndex(e => e.MemberId, "IX_GroupMemberJoinCode_MemberId");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.ExpiracyDate).HasColumnType("datetime");
            entity.Property(e => e.InsertedDate).HasColumnType("datetime");
            entity.Property(e => e.InsertedFrom).HasMaxLength(128);
            entity.Property(e => e.MemberId).HasMaxLength(128);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedFrom).HasMaxLength(128);

            entity.HasOne(d => d.Group).WithMany(p => p.GroupMemberJoinCodes)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupMemberJoinCode_Group");

            entity.HasOne(d => d.Member).WithMany(p => p.GroupMemberJoinCodes)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupMemberJoinCode_AspNetUsers");
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.ToTable("Hospital");

            entity.HasIndex(e => e.CityId, "IX_Hospital_CityId");

            entity.Property(e => e.Address).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(64);

            entity.HasOne(d => d.City).WithMany(p => p.Hospitals)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hospital_City");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasIndex(e => e.NotificationTypeId, "IX_Notifications_NotificationTypeId");

            entity.HasIndex(e => e.SenderId, "IX_Notifications_SenderId");

            entity.Property(e => e.SenderId).HasMaxLength(128);

            entity.HasOne(d => d.NotificationType).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.NotificationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Sender).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Notifications_AspNetUsers");
        });

        modelBuilder.Entity<NotificationUser>(entity =>
        {
            entity.HasIndex(e => e.NotificationId, "IX_NotificationUsers_NotificationId");

            entity.HasIndex(e => e.UserId, "IX_NotificationUsers_UserId");

            entity.Property(e => e.UserId).HasMaxLength(128);

            entity.HasOne(d => d.Notification).WithMany(p => p.NotificationUsers)
                .HasForeignKey(d => d.NotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.NotificationUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NotificationUsers_AspNetUsers_UserId");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AspNetRoles");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<RoleClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AspNetRoleClaims");

            entity.HasIndex(e => e.RoleId, "IX_RoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleClaims)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AspNetRoleClaims_AspNetRoles_RoleId");
        });

        modelBuilder.Entity<SuccessStory>(entity =>
        {
            entity.ToTable("SuccessStory");

            entity.Property(e => e.Description).HasMaxLength(1024);
            entity.Property(e => e.InsertedDate).HasColumnType("datetime");
            entity.Property(e => e.InsertedFrom).HasMaxLength(128);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedFrom).HasMaxLength(128);
            entity.Property(e => e.Title).HasMaxLength(256);
        });

        modelBuilder.Entity<SuccessStoryFile>(entity =>
        {
            entity.ToTable("SuccessStoryFile");

            entity.HasIndex(e => e.SuccessStoryId, "IX_SuccessStoryFile_SuccessStoryId");

            entity.Property(e => e.InsertedDate).HasColumnType("datetime");
            entity.Property(e => e.InsertedFrom).HasMaxLength(128);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedFrom).HasMaxLength(128);

            entity.HasOne(d => d.SuccessStory).WithMany(p => p.SuccessStoryFiles)
                .HasForeignKey(d => d.SuccessStoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SuccessStoryFile_SuccessStory");
        });

        modelBuilder.Entity<UrgenceType>(entity =>
        {
            entity.ToTable("UrgenceType");

            entity.Property(e => e.Type).HasMaxLength(10);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AspNetUser");

            entity.HasIndex(e => e.BloodTypeId, "IX_Users_BloodTypeId");

            entity.HasIndex(e => e.GenderId, "IX_Users_GenderId");

            entity.Property(e => e.Id).HasMaxLength(128);
            entity.Property(e => e.Birthdate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.FirstName).HasMaxLength(64);
            entity.Property(e => e.InsertedDate).HasColumnType("datetime");
            entity.Property(e => e.LastDonationDate).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(64);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.PhoneNumber).HasMaxLength(32);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.BloodType).WithMany(p => p.Users)
                .HasForeignKey(d => d.BloodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AspNetUsers_BloodType");

            entity.HasOne(d => d.Gender).WithMany(p => p.Users)
                .HasForeignKey(d => d.GenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AspNetUsers_Gender");
        });

        modelBuilder.Entity<UserCity>(entity =>
        {
            entity.ToTable("UserCity");

            entity.HasIndex(e => e.CityId, "IX_UserCity_CityId");

            entity.HasIndex(e => e.UserId, "IX_UserCity_UserId");

            entity.Property(e => e.InsertedDate).HasColumnType("datetime");
            entity.Property(e => e.InsertedFrom).HasMaxLength(128);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedFrom).HasMaxLength(128);
            entity.Property(e => e.UserId).HasMaxLength(128);

            entity.HasOne(d => d.City).WithMany(p => p.UserCities)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCity_City");

            entity.HasOne(d => d.User).WithMany(p => p.UserCities)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserCity_AspNetUsers");
        });

        modelBuilder.Entity<UserClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AspNetUserClaims");

            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<UserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey }).HasName("PK_AspNetUserLogins");

            entity.Property(e => e.UserId).HasMaxLength(450);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId }).HasName("PK_AspNetUserRoles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
