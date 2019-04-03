using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVLCarSystemApp.DataAccessInterfaces;
using AVLCarSystemApp.DataContexts;
using AVLCarSystemApp.ModelsDTO;
using Microsoft.AspNetCore.Hosting.Internal;

namespace AVLCarSystemApp.DataAccessImplementations
{
  public class EquipmentRepository : Repository<EquipmentDto>, IEquipmentRepository
  {

    public EquipmentRepository(CarSystemContext context) : base(context)
    {
    }

    public CarSystemContext CarSystemContext
    {
      get { return Context as CarSystemContext; }
    }

    public override bool CanRemove(EquipmentDto entity)
    {
      return !Context.Set<ModelDto>().Any(s => s.EquipmentId == entity.Id);
    }
  }
}
