using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVLCarSystemApp.ModelsDTO;

namespace AVLCarSystemApp.DataAccessInterfaces
{
  public interface ICountryRepository : IRepository<CountryDto>
  {
    CountryDto GetByName(string name);
  }
}
