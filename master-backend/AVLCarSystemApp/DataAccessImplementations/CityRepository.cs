using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVLCarSystemApp.DataAccessInterfaces;
using AVLCarSystemApp.DataContexts;
using AVLCarSystemApp.ModelsDTO;

namespace AVLCarSystemApp.DataAccessImplementations
{
  public class CityRepository : Repository<CityDto>, ICityRepository
  {

    public CityRepository(CarSystemContext context) : base(context)
    {
    }


    public CarSystemContext CarSystemContext
    {
      get { return Context as CarSystemContext; }
    }

    
    public override bool CanRemove(CityDto entity)
    {
      return !(Context.Set<SalonDto>().Any(s => s.CityId == entity.Id) ||
              Context.Set<ManufacturerDto>().Any(s => s.CityId == entity.Id));
    }

   

  }
}
