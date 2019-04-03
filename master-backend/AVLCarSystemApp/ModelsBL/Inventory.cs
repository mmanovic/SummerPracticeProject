using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVLCarSystemApp.ModelsBL
{
    public class Inventory
    {
        public long Id { get; set; }

        public long ModelId { get; set; }

        public long SalonId { get; set; }

        public decimal Price { get; set; }

        public int NumberOfUnitsOnStock { get; set; }
    }
}
