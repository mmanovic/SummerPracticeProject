using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using AutoMapper;
using AutoMapper.Configuration;
using AVLCarSystemApp.AutoMapperFolder;
using AVLCarSystemApp.Controllers;
using AVLCarSystemApp.DataAccessInterfaces;
using AVLCarSystemApp.ModelsBL;
using AVLCarSystemApp.ModelsDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace TestProject
{
  public class CityControllerTest
  {
    private static readonly IMapper Mapper = new Mapper(
        new MapperConfiguration(new MappingProfileTest()));



    [Fact]
    public void GetReturnsNotFound()
    {
      var mock = new Mock<ICityRepository>();
      var controller = new CityController(mock.Object, Mapper);

      IActionResult result = controller.Get(1);
      Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GetReturnsValidCity()
    {
      var cityDto = new CityDto()
      {
        Id = 23,
        Name = "name",
        Description = "descript"
      };

      var mock = new Mock<ICityRepository>();
      mock.Setup(s => s.Get(23)).Returns(cityDto);

      var controller = new CityController(mock.Object, Mapper);
      var result = controller.Get(23);
      Assert.NotNull(result);
      Assert.IsType<OkObjectResult>(result);

      var city = (City)((OkObjectResult)result).Value;
      Assert.Equal(23L, city.Id);
      Assert.Equal(cityDto.Name, city.Name);
      Assert.Equal(cityDto.Description, city.Description);
    }

    [Fact]
    public void GetAllReturnsAllCities()
    {
      var objs = new[]
      {
        new CityDto() {Name = "name1"},
        new CityDto() {Name = "name2"}
      };

      var mock = new Mock<ICityRepository>();
      mock.Setup(s => s.GetAll()).Returns(objs);

      var controller = new CityController(mock.Object, Mapper);
      var result = controller.GetAll();
      Assert.NotNull(result);
      Assert.IsType<OkObjectResult>(result);

      var coll = (IEnumerable<City>)((OkObjectResult)result).Value;
      Assert.Equal(2, coll.Count());
    }

    [Fact]
    public void PostReturns201Status()
    {
      var mock = new Mock<ICityRepository>();
      mock.Setup(s => s.Add(It.IsAny<CityDto>()));

      var controller = new CityController(mock.Object, Mapper);
      var result = controller.Post(new City() { Name = "name" });

      Assert.NotNull(result);
      Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void PutReturnsBadRequestStatus()
    {
      var mock = new Mock<ICityRepository>();
      mock.Setup(s => s.Update(It.IsAny<CityDto>()));

      var controller = new CityController(mock.Object, Mapper);
      var result = controller.Put(2, new City() { Id = 1, Name = "asdf" });

      Assert.NotNull(result);
      Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void PutReturnsNoContentAtSuccesfullUpdate()
    {
      var mock = new Mock<ICityRepository>();
      mock.Setup(s => s.Update(It.IsAny<CityDto>()));

      var controller = new CityController(mock.Object, Mapper);
      try
      {
        var result = controller.Put(1, new City() { Id = 1, Name = "asdf" });
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
      }
      catch (Exception e)
      {
        // failed test
        Assert.False(true);
      }
    }


    [Fact]
    public void DeleteReturnsNotFound()
    {
      var mock = new Mock<ICityRepository>();
      mock.Setup(s => s.Remove(It.IsAny<CityDto>()));
      mock.Setup(s => s.Find(It.IsAny<Expression<Func<CityDto, bool>>>()))
        .Returns(new List<CityDto>());

      var controller = new CityController(mock.Object, Mapper);
      var result = controller.Delete(1);

      Assert.NotNull(result);
      Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void DeleteReturnsOkResultAtSuccess()
    {
      var mock = new Mock<ICityRepository>();
      mock.Setup(s => s.Remove(It.IsAny<CityDto>()));
      mock.Setup(s => s.CanRemove(It.IsAny<CityDto>())).Returns(true);
      mock.Setup(s => s.Find(It.IsAny<Expression<Func<CityDto, bool>>>()))
        .Returns(new[]
        {
          new CityDto() { Id = 1, Name = "test name"}
        });

      var controller = new CityController(mock.Object, Mapper);
      var result = controller.Delete(1);

      Assert.NotNull(result);
      Assert.IsType<OkObjectResult>(result);
    }
  }
}