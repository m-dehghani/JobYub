using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobYub.Models
{
    public class CompanyType
    {
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }

		public virtual ICollection<AdvertisementCompanyType> AdvertisementCompanyTypes { get; set; }
	}
}
