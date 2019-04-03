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
  public class EngineController : Controller
  {
    private readonly IEngineRepository _repo;
    private readonly IMapper _mapper;

    public EngineController(IEngineRepository repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }

    // GET: api/Engine
    [HttpGet]
    [Authorize(Roles = "admin")]

    public IActionResult GetAll([FromQuery] long? engineTypeId)
    {
      return ControllerUtil.GetFiltered<EngineDto, Engine>(this, _repo, _mapper,
        x => (engineTypeId == null || x.EngineTypeId == engineTypeId));
    }

    // GET api/<controller>/5
    [HttpGet("{id:long}")]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult Get(long id)
    {
      return ControllerUtil.Get<EngineDto, Engine>(this, _repo, _mapper, id);
    }

    // POST api/<controller>
    [HttpPost]
    [Authorize(Roles = "admin")]

    public IActionResult Post([FromBody]Engine engine)
    {
      return ControllerUtil.Post(this, _repo, _mapper, engine, model => new { Id = model.Id });
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]

    public IActionResult Put(long id, [FromBody] Engine engine)
    {
      return ControllerUtil.Put(this, _repo, _mapper, engine, dto => dto.Id == id);
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]

    public IActionResult Delete([FromRoute] long id)
    {
      return ControllerUtil.Delete<EngineDto, Engine>(this, _repo, _mapper, dto => dto.Id == id);
    }
  }
}
