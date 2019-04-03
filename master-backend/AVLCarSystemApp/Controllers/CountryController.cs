using System;
using System.Collections.Generic;
using System.Linq;
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
  [Route("api/Country")]
  public class CountryController : Controller
  {

    private readonly ICountryRepository _repo;
    private readonly IMapper _mapper;

    public CountryController(ICountryRepository repo, IMapper mapper)
    {
      _repo = repo;
      _mapper = mapper;
    }

    // GET: api/Country
    [HttpGet]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult GetAll()
    {
      return ControllerUtil.GetAll<CountryDto, Country>(this, _repo, _mapper);
    }

    // GET: api/Country/5
    [HttpGet("{id:long}", Name = "Get")]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult Get(long id)
    {
      return ControllerUtil.Get<CountryDto, Country>(this, _repo, _mapper, id);
    }


    // GET: api/Country/Croatia
    [HttpGet("{name}", Name = "GetName")]
    [Authorize(Roles = "admin,employee,client")]
    public IActionResult Get(string name)
    {
      try
      {
        CountryDto countryDto = _repo.GetByName(name);
        if (countryDto == null)
        {
          return NotFound();
        }

        var country = _mapper.Map<CountryDto, Country>(countryDto);
        return Ok(country);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // POST: api/Country
    [HttpPost]
    [Authorize(Roles = "admin")]
    public IActionResult Post([FromBody] Country country)
    {
      return ControllerUtil.Post(this, _repo, _mapper, country, model => new {Id = model.Id});
    }

    // PUT: api/Country/5
    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Put(long id, [FromBody] Country country)
    {
      return ControllerUtil.Put(this, _repo, _mapper, country, modelDto => modelDto.Id == id);
    }

    // DELETE: api/Country
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult Delete([FromRoute] int id)
    {
      return ControllerUtil.Delete<CountryDto, Country>(this, _repo, _mapper, modelDto => modelDto.Id == id);
    }
  }
}

