using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AVLCarSystemApp.ModelsDTO
{
    public class CountryDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<SalonDto> Salons { get; set; }

        public ICollection<ManufacturerDto> Manufacturers { get; set; }
    }
}
