using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVLCarSystemApp.DataAccessInterfaces;
using AVLCarSystemApp.DataContexts;
using AVLCarSystemApp.ModelsDTO;

namespace AVLCarSystemApp.DataAccessImplementations
{
  public class SalonRepository : Repository<SalonDto>, ISalonRepository
  {

    public SalonRepository(CarSystemContext context)
        : base(context)
    {
    }

    public CarSystemContext CarSystemContext
    {
      get { return Context as CarSystemContext; }
    }

    public override void Remove(SalonDto entity)
    {
      // manualy delete inventories
      IEnumerable<InventoryDto> inventories = Context.Set<InventoryDto>()
        .Where(inventory => inventory.SalonId == entity.Id);
      Context.Set<InventoryDto>().RemoveRange(inventories);

      // now salon can be deleted and saved
      base.Remove(entity);
    }

    //public override void RemoveRange(IEnumerable<SalonDto> entities)
    //{
    //  // remove all inventories
    //  foreach (SalonDto entity in entities)
    //    Delete(entity);
    //}

  }
}
