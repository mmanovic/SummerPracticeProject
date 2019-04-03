using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AVLCarSystemApp.ModelsDTO
{
    public class EquipmentDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(200)]
        public string Color { get; set; }

        public bool AirConditioning { get; set; }

        public bool AutomaticTransmission { get; set; }

        public ICollection<ModelDto> Models { get; set; }
    }
}
