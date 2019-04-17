using System;
using System.Collections.Generic;
using System.Text;
using JobYub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobYub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin,
        ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            builder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });


            builder.Entity<AdvertisementMajor>().HasKey(am => new { am.AdvertisementID, am.MajorID });
            builder.Entity<AdvertisementEducationLevel>().HasKey(ae => new { ae.AdvertisementID, ae.EducationLevelID });
            builder.Entity<AdvertisementCompanyType>().HasKey(ae => new { ae.AdvertisementID, ae.CompanyTypeID });


        }
        public DbSet<JobYub.Models.City> City { get; set; }
        public DbSet<JobYub.Models.Advertisement> Advertisement { get; set; }
        public DbSet<JobYub.Models.CompanyType> CompanyType { get; set; }
        public DbSet<JobYub.Models.JobCategory> JobCategory { get; set; }
        public DbSet<JobYub.Models.Province> Province { get; set; }
        public DbSet<JobYub.Models.Region> Region { get; set; }
        public DbSet<JobYub.Models.Tag> Tag { get; set; }
        public DbSet<JobYub.Models.Tarrif> Tarrif { get; set; }
        public DbSet<JobYub.Models.Payment> Payment { get; set; }
        public DbSet<JobYub.Models.Major> Major { get; set; }
		public DbSet<JobYub.Models.ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<EducationLevel> EducationLevel { get; set; }
        public DbSet<AdvertisementEducationLevel> AdvertisementEducationLevels { get; set; }
        public DbSet<AdvertisementMajor> AdvertisementMajors { get; set; }
        public DbSet<AdvertisementCompanyType> AdvertisementCompanyTypes { get; set; }


    }
}
