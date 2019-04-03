using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AVLCarSystemApp.DataAccessInterfaces;
using AVLCarSystemApp.ModelsBL;
using AVLCarSystemApp.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AVLCarSystemApp.Controllers
{
  [Route("api/[controller]")]
  public class ModelController : Controller
  {
    private readonly IModelRepository _repo;
    private readonly IMapper _mapper;

    public ModelController(IModelRepository repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }


    // GET: api/Model
    [HttpGet]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult GetAll([FromQuery]long? manufacturerId, [FromQuery]long? equipmentId, [FromQuery]long? engineId)
    {
      return ControllerUtil.GetFiltered<ModelDto, Model>(this, _repo, _mapper, x =>
          (manufacturerId == null || x.ManufacturerId == manufacturerId)
          && (equipmentId == null || x.EquipmentId == equipmentId)
          && (engineId == null || x.EngineId == engineId));
    }

    // GET api/<controller>/5
    [HttpGet("{id:long}")]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult Get(long id)
    {
      return ControllerUtil.Get<ModelDto, Model>(this, _repo, _mapper, id);
    }

    // POST api/<controller>
    [HttpPost]
    [Authorize(Roles = "admin")]
    public IActionResult Post([FromBody]Model model)
    {
      return ControllerUtil.Post(this, _repo, _mapper, model, m => new { Id = m.Id });
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Put(long id, [FromBody] Model model)
    {
      return ControllerUtil.Put(this, _repo, _mapper, model, dto => dto.Id == id);
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Delete([FromRoute] long id)
    {
      return ControllerUtil.Delete<ModelDto, Model>(this, _repo, _mapper, dto => dto.Id == id);
    }
  }
}
