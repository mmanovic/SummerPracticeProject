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
  public class CountryControllerTest
  {
    private static readonly IMapper Mapper = new Mapper(
    new MapperConfiguration(new MappingProfileTest()));

    [Fact]
    public void GetOverIdReturnsNotFound()
    {
      var mock = new Mock<ICountryRepository>();
      var controller = new CountryController(mock.Object, Mapper);

      IActionResult result = controller.Get(1);
      Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GetOverStringReturnsNotFound()
    {
      var mock = new Mock<ICountryRepository>();
      var controller = new CountryController(mock.Object, Mapper);

      IActionResult result = controller.Get("lala Land");
      Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void GetReturnsValidCountry()
    {
      var countryDto = new CountryDto()
      {
        Id = 3,
        Name = "name3",
        Description = "lala land"
      };

      var mock = new Mock<ICountryRepository>();
      mock.Setup(s => s.Get(3)).Returns(countryDto);

      var controller = new CountryController(mock.Object, Mapper);
      var result = controller.Get(3);
      Assert.NotNull(result);
      Assert.IsType<OkObjectResult>(result);

      var country = (Country)((OkObjectResult)result).Value;
      Assert.Equal(3L, country.Id);
      Assert.Equal(countryDto.Name, country.Name);
      Assert.Equal(countryDto.Description, country.Description);
    }

    [Fact]
    public void GetAllReturnsAllCountries()
    {
      var countries = new[]
      {
        new CountryDto() {Name = "lala1"},
        new CountryDto() {Name = "lala2"}
      };

      var mock = new Mock<ICountryRepository>();
      mock.Setup(s => s.GetAll()).Returns(countries);

      var controller = new CountryController(mock.Object, Mapper);
      var result = controller.GetAll();
      Assert.NotNull(result);
      Assert.IsType<OkObjectResult>(result);

      var collection = (IEnumerable<Country>)((OkObjectResult)result).Value;
      Assert.Equal(2, collection.Count());
    }

    [Fact]
    public void GetAllReturnsNonCountries()
    {
      var mock = new Mock<ICountryRepository>();
      mock.Setup(s => s.GetAll()).Returns(Enumerable.Empty<CountryDto>);

      var controller = new CountryController(mock.Object, Mapper);
      var result = controller.GetAll();
      Assert.NotNull(result);
      Assert.IsType<OkObjectResult>(result);

      var collection = (IEnumerable<Country>)((OkObjectResult)result).Value;
      Assert.Empty(collection);
    }

    [Fact]
    public void PostReturns201Status()
    {
      var mock = new Mock<ICountryRepository>();
      mock.Setup(s => s.Add(It.IsAny<CountryDto>()));

      var controller = new CountryController(mock.Object, Mapper);
      var result = controller.Post(new Country() { Name = "lala" });

      Assert.NotNull(result);
      Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void PutReturnsBadRequestStatus()
    {
      var mock = new Mock<ICountryRepository>();
      mock.Setup(s => s.Add(It.IsAny<CountryDto>()));

      var controller = new CountryController(mock.Object, Mapper);
      var result = controller.Put(2, new Country() { Id = 1, Name = "lala" });

      Assert.NotNull(result);
      Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void PutReturnsNoContentAtSuccesfullUpdate()
    {
      var mock = new Mock<ICountryRepository>();
      mock.Setup(s => s.Update(It.IsAny<CountryDto>()));

      var controller = new CountryController(mock.Object, Mapper);
      try
      {
        var result = controller.Put(1, new Country() { Id = 1, Name = "lala" });
        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
      }
      catch (Exception e)
      {
        Assert.False(true);
      }
    }

    [Fact]
    public void DeleteReturnsNotFound()
    {
      var mock = new Mock<ICountryRepository>();
      mock.Setup(s => s.Remove(It.IsAny<CountryDto>()));
      mock.Setup(s => s.Find(It.IsAny<Expression<Func<CountryDto, bool>>>()))
        .Returns(new List<CountryDto>());


      var controller = new CountryController(mock.Object, Mapper);
      var result = controller.Delete(1);

      Assert.NotNull(result);
      Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void DeleteReturnsOkResultAtSuccess()
    {
      var mock = new Mock<ICountryRepository>();
      mock.Setup(s => s.Remove(It.IsAny<CountryDto>()));
      mock.Setup(s => s.CanRemove(It.IsAny<CountryDto>())).Returns(true);
      mock.Setup(s => s.Find(It.IsAny<Expression<Func<CountryDto, bool>>>()))
          .Returns(new[]
          {
                    new CountryDto() { Id = 1, Name = "lala"}
          });

      var controller = new CountryController(mock.Object, Mapper);
      var result = controller.Delete(1);

      Assert.NotNull(result);
      Assert.IsType<OkObjectResult>(result);
    }
  }
}