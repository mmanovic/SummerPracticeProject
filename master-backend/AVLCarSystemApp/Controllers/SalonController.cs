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
  public class SalonController : Controller
  {
    private readonly ISalonRepository _repo;
    private readonly IMapper _mapper;

    public SalonController(ISalonRepository repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }

    // GET: api/Salon
    [HttpGet]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult GetAll([FromQuery]long? cityId, [FromQuery]long? countryId)
    {
      return ControllerUtil.GetFiltered<SalonDto, Salon>(this, _repo, _mapper, x =>
          (cityId == null || x.CityId == cityId) &&
          (countryId == null || x.CountryId == countryId));
    }

    // GET api/<controller>/5
    [HttpGet("{id:long}")]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult Get(long id)
    {
      return ControllerUtil.Get<SalonDto, Salon>(this, _repo, _mapper, id);
    }

    // POST api/<controller>
    [HttpPost]
    [Authorize(Roles = "admin")]
    public IActionResult Post([FromBody]Salon salon)
    {
      return ControllerUtil.Post(this, _repo, _mapper, salon, m => new { Id = m.Id });
    }

    // PUT api/<controller>/5
    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Put(long id, [FromBody] Salon salon)
    {
      return ControllerUtil.Put(this, _repo, _mapper, salon, dto => dto.Id == id);
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Delete([FromRoute] long id)
    {
      return ControllerUtil.Delete<SalonDto, Salon>(this, _repo, _mapper, dto => dto.Id == id);
    }
  }
}
