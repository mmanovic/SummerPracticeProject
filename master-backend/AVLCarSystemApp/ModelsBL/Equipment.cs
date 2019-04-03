using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AVLCarSystemApp.ModelsBL
{
    public class Equipment
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(200)]
        public string Color { get; set; }

        public bool AirConditioning { get; set; }

        public bool AutomaticTransmission { get; set; }
    }
}
