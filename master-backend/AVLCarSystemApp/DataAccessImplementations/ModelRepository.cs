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
  public class ModelRepository : Repository<ModelDto>, IModelRepository
  {

    public ModelRepository(CarSystemContext context) : base(context)
    {
    }

    public CarSystemContext CarSystemContext
    {
      get { return Context as CarSystemContext; }
    }

    public override void Remove(ModelDto entity)
    {
      // manualy delete inventories
      IEnumerable<InventoryDto> inventories = Context.Set<InventoryDto>()
        .Where(inventory => inventory.ModelId == entity.Id);
      Context.Set<InventoryDto>().RemoveRange(inventories);

      // now model can be deleted
      base.Remove(entity);
    }

    //public override void RemoveRange(IEnumerable<ModelDto> entities)
    //{
    //  foreach (ModelDto entity in entities)
    //    Delete(entity);
    //}
  }
}
