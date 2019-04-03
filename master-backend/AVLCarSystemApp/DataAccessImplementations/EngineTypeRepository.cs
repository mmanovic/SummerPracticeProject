using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVLCarSystemApp.DataAccessInterfaces;
using AVLCarSystemApp.DataContexts;
using AVLCarSystemApp.ModelsDTO;

namespace AVLCarSystemApp.DataAccessImplementations
{
  public class EngineTypeRepository : Repository<EngineTypeDto>, IEngineTypeRepository
  {

    public EngineTypeRepository(CarSystemContext context) : base(context)
    {
    }

    public CarSystemContext CarSystemContext
    {
      get { return Context as CarSystemContext; }
    }

    public override bool CanRemove(EngineTypeDto entity)
    {
      return !Context.Set<EngineDto>().Any(s => s.EngineTypeId == entity.Id);
    }
  }
}
