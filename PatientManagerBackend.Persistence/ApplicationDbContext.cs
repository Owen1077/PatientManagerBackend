using PatientManagerBackend.Domain.Entities;
using PatientManagerBackend.Domain.Entities.Base;
using PatientManagerBackend.Domain.Enum;
using PatientManagerBackend.Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using System.Data;
using System;
using System.Reflection.Emit;

namespace PatientManagerBackend.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly DatabaseOptions _databaseOptions;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
            IOptions<DatabaseOptions> databaseOptions) : base(options)
        {
            _databaseOptions = databaseOptions.Value;
        }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientRecord> PatientRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }






        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.HasDefaultSchema(_databaseOptions.SchemaName);

        //    modelBuilder.Entity<User>(entity =>
        //    {
        //        entity.ToTable(name: "USER");
        //        entity.Property(x => x.Id)
        //            .ValueGeneratedOnAdd()
        //            .HasColumnName("ID");
        //        entity.Property(x => x.AccessFailedCount)
        //            .HasColumnName("ACCESS_FAILED_COUNT");
        //        entity.Property(x => x.ConcurrencyStamp)
        //            .HasColumnName("CONCURRENCY_STAMP");
        //        entity.Property(x => x.CreatedAt)
        //            .HasColumnName("CREATED_AT");
        //        entity.Property(x => x.UpdatedAt)
        //            .HasColumnName("UPDATED_AT");
        //        entity.Property(x => x.Email)
        //            .HasColumnName("EMAIL");
        //        entity.Property(x => x.EmailConfirmed)
        //            .HasColumnName("EMAIL_CONFIRMED");
        //        entity.Property(x => x.Name)
        //            .HasColumnName("NAME");
        //        entity.Property(x => x.Status)
        //            .HasConversion(new EnumToStringConverter<UserStatus>())
        //            .HasColumnName("STATUS");
        //        entity.Property(x => x.IsLoggedIn)
        //            .HasColumnName("IS_LOGGED_IN");
        //        entity.Property(x => x.LastLoginTime)
        //            .HasColumnName("LAST_LOGIN_TIME");
        //        entity.Property(x => x.LockoutEnabled)
        //            .HasColumnName("LOCKOUT_ENABLED");
        //        entity.Property(x => x.LockoutEnd)
        //            .HasColumnName("LOCKOUT_END");
        //        entity.Property(x => x.NormalizedEmail)
        //            .HasColumnName("NORMALIZED_EMAIL");
        //        entity.Property(x => x.NormalizedUserName)
        //            .HasColumnName("NORMALIZED_USER_NAME");
        //        entity.Property(x => x.PasswordHash)
        //            .HasColumnName("PASSWORD_HASH");
        //        entity.Property(x => x.PhoneNumber)
        //            .HasColumnName("PHONE_NUMBER");
        //        entity.Property(x => x.PhoneNumberConfirmed)
        //            .HasColumnName("PHONE_NUMBER_CONFIRMED");
        //        entity.Property(x => x.SecurityStamp)
        //            .HasColumnName("SECURITY_STAMP");
        //        entity.Property(x => x.TwoFactorEnabled)
        //            .HasColumnName("TWO_FACTOR_ENABLED");
        //        entity.Property(x => x.UserName)
        //            .HasColumnName("USER_NAME");

        //        // Each User can have many UserClaims
        //        entity.HasMany(e => e.Claims)
        //            .WithOne()
        //            .HasForeignKey(uc => uc.UserId)
        //            .IsRequired();

        //        // Each User can have many UserLogins
        //        entity.HasMany(e => e.Logins)
        //            .WithOne()
        //            .HasForeignKey(ul => ul.UserId)
        //            .IsRequired();

        //        // Each User can have many UserTokens
        //        entity.HasMany(e => e.Tokens)
        //            .WithOne()
        //            .HasForeignKey(ut => ut.UserId)
        //            .IsRequired();

        //        entity.HasMany(e => e.UserRoles)
        //            .WithOne(ur => ur.User)
        //            .HasForeignKey(ur => ur.UserId)
        //            .IsRequired();
        //    }).Model.SetMaxIdentifierLength(30);

        //    modelBuilder.Entity<Role>(entity =>
        //    {
        //        entity.ToTable(name: "ROLE");
        //        entity.HasKey(x => x.Id);
        //        entity.Property(x => x.Id)
        //            .HasColumnName("ID");
        //        entity.Property(x => x.ConcurrencyStamp)
        //            .HasColumnName("CONCURRENCY_STAMP");
        //        entity.Property(x => x.Name)
        //            .HasColumnName("NAME");
        //        entity.Property(x => x.NormalizedName)
        //            .HasColumnName("NORMALIZED_NAME");
        //        entity.HasMany(e => e.UserRoles)
        //            .WithOne(ur => ur.Role)
        //            .HasForeignKey(ur => ur.RoleId)
        //            .IsRequired();
        //    }).Model.SetMaxIdentifierLength(30);

        //    modelBuilder.Entity<UserRole>(entity =>
        //    {
        //        entity.HasKey(ur => new { ur.UserId, ur.RoleId });
        //        entity.ToTable("USER_ROLES");
        //        entity.Property(x => x.RoleId)
        //            .HasColumnName("ROLE_ID");
        //        entity.Property(x => x.UserId)
        //            .HasColumnName("USER_ID");
        //    }).Model.SetMaxIdentifierLength(30);


        //    modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
        //    {
        //        entity.ToTable("USER_CLAIMS");
        //        entity.Property(x => x.ClaimType)
        //            .HasColumnName("CLAIM_TYPE");
        //        entity.Property(x => x.ClaimValue)
        //            .HasColumnName("CLAIM_VALUE");
        //        entity.Property(x => x.Id)
        //            .HasColumnName("ID");
        //        entity.Property(x => x.UserId)
        //            .HasColumnName("USER_ID");
        //    }).Model.SetMaxIdentifierLength(30);

        //    modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
        //    {
        //        entity.ToTable("ROLE_CLAIMS");
        //        entity.Property(x => x.ClaimType)
        //            .HasColumnName("CLAIM_TYPE");
        //        entity.Property(x => x.ClaimValue)
        //            .HasColumnName("CLAIM_VALUE");
        //        entity.Property(x => x.Id)
        //            .HasColumnName("ID");
        //        entity.Property(x => x.RoleId)
        //            .HasColumnName("ROLE_ID");
        //    }).Model.SetMaxIdentifierLength(30);

        //    modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
        //    {
        //        entity.ToTable("USER_LOGINS");
        //        entity.Property(x => x.LoginProvider)
        //            .HasColumnName("LOGIN_PROVIDER");
        //        entity.Property(x => x.ProviderDisplayName)
        //            .HasColumnName("PROVIDER_DISPLAY_NAME");
        //        entity.Property(x => x.ProviderKey)
        //            .HasColumnName("PROVIDER_KEY");
        //        entity.Property(x => x.UserId)
        //            .HasColumnName("USER_ID");
        //    }).Model.SetMaxIdentifierLength(30);

        //    modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        //    {
        //        entity.ToTable("USER_TOKENS");
        //        entity.Property(x => x.LoginProvider)
        //            .HasColumnName("LOGIN_PROVIDER");
        //        entity.Property(x => x.Name)
        //            .HasColumnName("NAME");
        //        entity.Property(x => x.Value)
        //            .HasColumnName("VALUE");
        //        entity.Property(x => x.UserId)
        //            .HasColumnName("USER_ID");
        //    }).Model.SetMaxIdentifierLength(30);

        //    modelBuilder.Seed();
        //}

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}