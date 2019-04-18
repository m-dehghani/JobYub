using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
       
       
    }
    public static class Extensions
    {
        public static string GetDescription(this Exception e)
        {
            var builder = new StringBuilder();
            AddException(builder, e);
            return builder.ToString();
        }
        private static void AddException(StringBuilder builder, Exception e)
        {
            builder.AppendLine($"Message: {e.Message}");
            builder.AppendLine($"Stack Trace: {e.StackTrace}");
            if (e.InnerException != null)
            {
                builder.AppendLine("Inner Exception");
                AddException(builder, e.InnerException);
            }
        }
    }

}
