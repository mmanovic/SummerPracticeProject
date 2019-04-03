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
  public class InventoryController : Controller
  {
    private readonly IInventoryRepository _repo;
    private readonly IMapper _mapper;

    public InventoryController(IInventoryRepository repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }

    // GET: api/Inventory
    [HttpGet]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult GetAll([FromQuery]long? salonId, [FromQuery]long? modelId)
    {
      return ControllerUtil.GetFiltered<InventoryDto, Inventory>(this, _repo, _mapper, x =>
        (salonId == null || x.SalonId == salonId) &&
        (modelId == null || x.ModelId == modelId));
    }

    // GET api/<controller>/5
    [HttpGet("{id:long}")]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult Get(long id)
    {
      return ControllerUtil.Get<InventoryDto, Inventory>(this, _repo, _mapper, id);
    }

    // POST api/<controller>
    [HttpPost]
    [Authorize(Roles = "admin,employee")]
    public IActionResult Post([FromBody]Inventory inventory)
    {
      return ControllerUtil.Post(this, _repo, _mapper, inventory, model => new { Id = model.Id });
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    [Authorize(Roles = "admin,employee")]
    public IActionResult Put(long id, [FromBody] Inventory inventory)
    {
      return ControllerUtil.Put(this, _repo, _mapper, inventory, dto => dto.Id == id);
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin,employee")]
    public IActionResult Delete([FromRoute] long id)
    {
      return ControllerUtil.Delete<InventoryDto, Inventory>(this, _repo, _mapper, dto => dto.Id == id);
    }
  }
}
