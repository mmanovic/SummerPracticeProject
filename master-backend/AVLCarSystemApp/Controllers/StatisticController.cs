using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AVLCarSystemApp.DataAccessInterfaces;
using AVLCarSystemApp.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace AVLCarSystemApp.Controllers
{
  [Produces("application/json")]
  [Route("api/Statistic")]
  [Authorize]
  public class StatisticController : Controller
  {
    private readonly IMapper _mapper;
    private readonly IManufacturerRepository _manufacturers;
    private readonly IModelRepository _models;
    private readonly IInventoryRepository _inventory;
    private readonly IEngineRepository _engines;
    private readonly IEngineTypeRepository _engineTypes;

    public StatisticController(IMapper mapper,
      IManufacturerRepository manufacturerRepository,
      IModelRepository modelRepository,
      IInventoryRepository inventoryRepository,
      IEngineTypeRepository engineTypeRepository,
      IEngineRepository engineRepository)
    {
      _mapper = mapper;
      _manufacturers = manufacturerRepository;
      _models = modelRepository;
      _inventory = inventoryRepository;
      _engines = engineRepository;
      _engineTypes = engineTypeRepository;
    }

    [HttpGet("AveragePricePerModels")]
    public IActionResult GetAverageModelPrices()
    {
      var prices = _models.GetAll()
        .Select(model => new
        {
          Id = model.Id,
          Name = model.Name,
          AveragePrice = SafeAverageValue(_inventory.Find(inv => inv.ModelId == model.Id), inv => inv.Price)
        });

      return Ok(prices);
    }

    [HttpGet("EngineTypePartsPerManufacturers")]
    public IActionResult GetEngineTypePartsInManufacturer()
    {
      IEnumerable<EngineDto> engines = _engines.GetAll();
      IEnumerable<EngineTypeDto> engineTypes = _engineTypes.GetAll();

      var result = _manufacturers.GetAll()
        .Select(m =>
        {
          var manufacturerModels = _models.Find(mm => mm.ManufacturerId == m.Id);
          return new
          {
            ManufacturerId = m.Id,
            ManufacturerName = m.Name,
            EngineTypeParts = engineTypes
                  .Join(engines,
                      et => et.Id,
                      e => e.EngineTypeId,
                      (et, e) => new { typesId = et.Id, enginesId = e.Id })
                  .Join(manufacturerModels,
                      at => at.enginesId,
                      model => model.EngineId,
                      (at, model) => new { model.Name, at.typesId })
                  .GroupBy(v => v.typesId)
                  .Select(s => new
                  {
                    EngineTypeId = s.Key,
                    Part = SafePartValue(s, manufacturerModels)
                  })
                  .OrderBy(o => o.EngineTypeId)
                  .ToDictionary(at => at.EngineTypeId, at => at.Part)
          };
        });

      var resultWithHeader = new
      {
        EngineTypes = engineTypes
              .Select(et => new { et.Id, et.Name })
              .OrderBy(o => o.Id),
        Table = result
      };

      return Ok(resultWithHeader);
    }

    private static double? SafePartValue(IEnumerable<dynamic> nominator, IEnumerable<dynamic> denominator)
    {
      long count = denominator.LongCount();
      return count == 0 ? (double?)null : nominator.LongCount() / (double)count;
    }

    private static double? SafeAverageValue<T>(IEnumerable<T> enumerable, Func<T, decimal> accFunc)
    {
      return enumerable.LongCount() == 0 ? null : (double?)enumerable.Average(accFunc);
    }
  }
}