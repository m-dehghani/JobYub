﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobYub.Models
{
	public class Major
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int ParentID { get; set; }
		public virtual Major ParentMajor { get; set; }
		public virtual ICollection<Advertisement> Advertisements { get; set; }
		public virtual ICollection<ApplicationUser> Users { get; set; }
	}
}