using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVLCarSystemApp.DataAccessInterfaces;
using AVLCarSystemApp.DataContexts;
using AVLCarSystemApp.ModelsDTO;

namespace AVLCarSystemApp.DataAccessImplementations
{
    public class InventoryRepository : Repository<InventoryDto>, IInventoryRepository
    {

        public InventoryRepository(CarSystemContext context) : base(context)
        {
        }

        public CarSystemContext CarSystemContext
        {
            get { return Context as CarSystemContext; }
        }

    }
}
