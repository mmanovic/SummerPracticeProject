using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AVLCarSystemApp.ModelsDTO
{
    public class EngineDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long EngineTypeId { get; set; }

        [ForeignKey("EngineTypeId")]
        public EngineTypeDto EngineTypeDto { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }

        public float Power { get; set; }

        public float FuelConsumption { get; set; }

        public ICollection<ModelDto> Models { get; set; }
    }
}
