using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobYub.Models
{
	public class AdvertisementCompanyType
	{
		
		public virtual int CompanyTypeID { get; set; }
		public virtual CompanyType CompanyType { get; set; }
		public virtual int AdvertisementID { get; set; }
		public virtual Advertisement Advertisement { get; set; }
	}
}
