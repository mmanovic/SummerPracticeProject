﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AVLCarSystemApp.ModelsBL
{
    public class Manufacturer
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        public long CityId { get; set; }


        public long CountryId { get; set; }
    }
}
