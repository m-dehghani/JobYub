using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobYub.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Website { get; set; }
		public string BirthDate { get; set; }
		public string HomePhone { get; set; }
		public string Mobile { get; set; }
        public string RawTextResume { get; set; }
        public virtual int? EducationLevelID { get; set; }
        public virtual EducationLevel EducationLevel { get; set; }
        public virtual int? MajorID { get; set; }
		public virtual Major Major { get; set; }
		public MilitaryStatus? MilitaryStatus { get; set; }
        public virtual int? CityID { get; set; }
        public virtual City City { get; set; }
        public virtual int? RegionID { get; set; }
        public virtual Region Region { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public byte[] Resume { get; set; }
        public bool IsPublic { get; set; } = true;
        public string Company { get; set; }
        public virtual int? CompanyTypeID { get; set; }
        public virtual CompanyType CompanyType { get; set; }
        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }
        public Gender Gender { get; set; }
        public bool Graduated { get; set; }
        public int? Experience { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
		public string VerificationCode { get; set; }
        public string Token { get; set; }
        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public  ICollection<ApplicationUserRole> UserRoles { get; set; }

    }
    public class ApplicationRole:IdentityRole

    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    }
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public virtual ApplicationUser User { get; set; }
    }

    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public virtual ApplicationUser User { get; set; }
    }

    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public virtual ApplicationRole Role { get; set; }
    }

    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
    public enum MilitaryStatus { DarHaleKhedmat, Moaaf, PayanKhedmat }
}
