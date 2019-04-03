using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVLCarSystemApp.DataAccessInterfaces;
using AVLCarSystemApp.DataContexts;
using AVLCarSystemApp.ModelsDTO;

namespace AVLCarSystemApp.DataAccessImplementations
{
  public class CountryRepository : Repository<CountryDto>, ICountryRepository
  {

    public CountryRepository(CarSystemContext context) : base(context)
    {
    }

    public CarSystemContext CarSystemContext
    {
      get { return Context as CarSystemContext; }
    }

    public CountryDto GetByName(string name)
    {
      return CarSystemContext.Countries.FirstOrDefault(s => s.Name.Equals(name));
    }

    public override bool CanRemove(CountryDto entity)
    {
      return !(Context.Set<SalonDto>().Any(s => s.CountryId == entity.Id) ||
              Context.Set<ManufacturerDto>().Any(s => s.CountryId == entity.Id));

    }

  }
}
