using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using AVLCarSystemApp.DataAccessInterfaces;
using AVLCarSystemApp.ModelsBL;
using AVLCarSystemApp.ModelsDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AVLCarSystemApp.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  public class CityController : Controller
  {
    private readonly ICityRepository _repo;
    private readonly IMapper _mapper;

    public CityController(ICityRepository repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }

    // GET: api/City
    [HttpGet]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult GetAll()
    {
      return ControllerUtil.GetAll<CityDto, City>(this, _repo, _mapper);
    }

    // GET: api/City/5
    [HttpGet("{id}")]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult Get(long id)
    {
      return ControllerUtil.Get<CityDto, City>(this, _repo, _mapper, id);
    }

    // POST: api/City
    [HttpPost]
    [Authorize(Roles = "admin")]
    public IActionResult Post([FromBody] City city)
    {
      return ControllerUtil.Post(this, _repo, _mapper, city, updatedModel => new {Id = updatedModel.Id});
    }

    // PUT: api/City/5
    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Put(long id, [FromBody] City city)
    {
      return ControllerUtil.Put(this, _repo, _mapper, city, cityDto => cityDto.Id == id);
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Delete([FromRoute] long id)
    {
      return ControllerUtil.Delete<CityDto, City>(this, _repo, _mapper, city => city.Id == id);
    }
  }
}
