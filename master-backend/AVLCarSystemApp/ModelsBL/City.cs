using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AVLCarSystemApp.ModelsBL
{
    public class City
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        public string Description { get; set; }
    }


}
