using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AVLCarSystemApp.ModelsDTO
{
    public class InventoryDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long? ModelId { get; set; }

        [ForeignKey("ModelId")]
        public ModelDto ModelDto { get; set; }

        public long? SalonId { get; set; }

        [ForeignKey("SalonId")]
        public SalonDto SalonDto { get; set; }

        public decimal Price { get; set; }

        public int NumberOfUnitsOnStock { get; set; }
    }
}
