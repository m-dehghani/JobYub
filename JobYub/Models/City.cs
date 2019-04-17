using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobYub.Models
{
    public class City
    {
		public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public virtual int ProvinceID { get; set; }
		public virtual Province Province { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
        // public virtual  ICollection<Advertisement> Advertisements { get; set; }
        // public virtual ICollection<Region> Regions { get; set; }
        public event EventHandler Citychanged;
        public void OnCityChange()
        {
            Citychanged(this, EventArgs.Empty);
        }
    }
   
}
