using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVLCarSystemApp.DataAccessInterfaces;
using AVLCarSystemApp.DataContexts;
using AVLCarSystemApp.ModelsDTO;

namespace AVLCarSystemApp.DataAccessImplementations
{
  public class ManufacturerRepository : Repository<ManufacturerDto>, IManufacturerRepository
  {

    public ManufacturerRepository(CarSystemContext context) : base(context)
    {
    }

    public CarSystemContext CarSystemContext
    {
      get { return Context as CarSystemContext; }
    }

    public override bool CanRemove(ManufacturerDto entity)
    {
      return !Context.Set<ModelDto>().Any(s => s.ManufacturerId == entity.Id);
    }
  }
}
