using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AVLCarSystemApp.ModelsDTO;

namespace AVLCarSystemApp.ModelsBL
{
    public class Engine
    {
        public long Id { get; set; }

        public long EngineTypeId { get; set; }

        public EngineTypeDto EngineTypeDto { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }

        public float Power { get; set; }

        public float FuelConsumption { get; set; }
    }
}
