﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobYub.Models
{
    public class EducationLevel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public virtual IEnumerable<AdvertisementEducationLevel> AdvertisementEducationLevels { set; get; }
    }
}
