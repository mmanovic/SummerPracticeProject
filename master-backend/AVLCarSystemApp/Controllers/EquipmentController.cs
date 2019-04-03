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
  public class EquipmentController : Controller
  {
    private readonly IEquipmentRepository _repo;
    private readonly IMapper _mapper;

    public EquipmentController(IEquipmentRepository repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }
    // GET: api/Equipment
    [HttpGet]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult GetAll()
    {
      return ControllerUtil.GetAll<EquipmentDto, Equipment>(this, _repo, _mapper);
    }

    // GET api/<controller>/5
    [HttpGet("{id:long}")]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult Get(long id)
    {
      return ControllerUtil.Get<EquipmentDto, Equipment>(this, _repo, _mapper, id);
    }

    // POST api/<controller>
    [HttpPost]
    [Authorize(Roles = "admin")]
    public IActionResult Post([FromBody]Equipment equipment)
    {
      return ControllerUtil.Post(this, _repo, _mapper, equipment, model => new { Id = model.Id });
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Put(long id, [FromBody] Equipment equipment)
    {
      return ControllerUtil.Put(this, _repo, _mapper, equipment, dto => dto.Id == id);
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Delete([FromRoute] long id)
    {
      return ControllerUtil.Delete<EquipmentDto, Equipment>(this, _repo, _mapper, dto => dto.Id == id);
    }
  }
}
