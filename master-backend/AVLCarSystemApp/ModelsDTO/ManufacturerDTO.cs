using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AVLCarSystemApp.ModelsDTO
{
    public class ManufacturerDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        public long CityId { get; set; }

        [ForeignKey("CityId")]
        public CityDto CityDto { get; set; }

        public long CountryId { get; set; }

        [ForeignKey("CountryId")]
        public CountryDto CountryDto { get; set; }

        public ICollection<ModelDto> Models { get; set; }
    }
}