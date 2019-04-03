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
  [Authorize(Roles = "admin")]
  public class EngineTypeController : Controller
  {

    private readonly IEngineTypeRepository _repo;
    private readonly IMapper _mapper;

    public EngineTypeController(IEngineTypeRepository repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }

    // GET: api/EngineType
    [HttpGet]
    public IActionResult GetAll()
    {
      return ControllerUtil.GetAll<EngineTypeDto, EngineType>(this, _repo, _mapper);
    }

    // GET api/<controller>/5
    [HttpGet("{id:long}")]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult Get(long id)
    {
      return ControllerUtil.Get<EngineTypeDto, EngineType>(this, _repo, _mapper, id);
    }

    // POST api/<controller>
    [HttpPost]
    public IActionResult Post([FromBody]EngineType engineType)
    {
      return ControllerUtil.Post(this, _repo, _mapper, engineType, model => new { Id = model.Id });
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    public IActionResult Put(long id, [FromBody] EngineType engineType)
    {
      return ControllerUtil.Put(this, _repo, _mapper, engineType, dto => dto.Id == id);
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] long id)
    {
      return ControllerUtil.Delete<EngineTypeDto, EngineType>(this, _repo, _mapper, dto => dto.Id == id);
    }
  }
}
