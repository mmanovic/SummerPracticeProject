using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AVLCarSystemApp.ModelsBL
{
    public class Model
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }

        public long ManufacturerId { get; set; }

        public long EngineId { get; set; }

        public long EquipmentId { get; set; }

        public short Year { get; set; }

        public short NumberOfDoors { get; set; }
    }
}
