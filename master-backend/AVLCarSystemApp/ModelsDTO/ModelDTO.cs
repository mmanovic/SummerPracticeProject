using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AVLCarSystemApp.ModelsDTO
{
    public class ModelDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long EquipmentId { get; set; }

        [ForeignKey("EquipmentId")]
        public EquipmentDto EquipmentDto { get; set; }

        public long EngineId { get; set; }

        [ForeignKey("EngineId")]
        public EngineDto EngineDto { get; set; }

        public long ManufacturerId { get; set; }

        [ForeignKey("ManufacturerId")]
        public ManufacturerDto ManufacturerDto { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }

        public short Year { get; set; }

        public short NumberOfDoors { get; set; }

        public ICollection<InventoryDto> Inventories { get; set; }
    }
}
