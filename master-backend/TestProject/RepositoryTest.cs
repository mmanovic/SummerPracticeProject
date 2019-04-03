using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVLCarSystemApp.DataAccessImplementations;
using AVLCarSystemApp.DataContexts;
using AVLCarSystemApp.ModelsDTO;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestProject
{
    public class RepositoryTest : InMemoryCarSystemTestBase
    {
        [Fact]
        public void GetNonExistingItem()
        {
            var service = new Repository<CityDto>(Context);
            var city = service.Get(1);
            Assert.Null(city);
        }

        [Fact]
        public void Get()
        {
            Context.Cities.Add(new CityDto()
            {
                Name = "City 1",
                Description = "Description 1"
            });
            Context.SaveChanges();

            var id = Context.Cities.First().Id;
            var service = new Repository<CityDto>(Context);
            var city = service.Get(id);

            Assert.Equal(id, city.Id);
            Assert.Equal("City 1", city.Name);
            Assert.Equal("Description 1", city.Description);
        }

        [Fact]
        public void GetAll()
        {
            var city1 = new CityDto()
            {
                Name = "City 1",
                Description = "Description 1"
            };
            var city2 = new CityDto()
            {
                Name = "City 2",
                Description = "Description 2"
            };
            
            Context.Set<CityDto>().Add(city1);
            Context.Set<CityDto>().Add(city2);
            Context.SaveChanges();

            var service = new Repository<CityDto>(Context);
            var cityCollection = service.GetAll();

            var cityDtos = cityCollection as CityDto[] ?? cityCollection.ToArray();
            Assert.Equal(2, cityDtos.Count());
            Assert.Contains(cityDtos, c => c.Name == city1.Name && c.Description == city1.Description);
            Assert.Contains(cityDtos, c => c.Name == city2.Name && c.Description == city2.Description);
        }

        [Fact]
        public void GetAllOnEmtyDb()
        {
            var service = new Repository<CityDto>(Context);
            var cityCollection = service.GetAll();

            Assert.NotNull(cityCollection);
            Assert.Empty(cityCollection);
        }


        [Fact]
        public void AddNullObject()
        {
            Assert.ThrowsAny<ArgumentNullException>(() => Context.Add(null));
        }

        [Fact]
        public void AddObjectTestCount()
        {
            var cityService = new Repository<CityDto>(Context);

            var count = Context.Cities.Count();
            cityService.Add(new CityDto()
            {
                Name = "Moscow",
                Description = "City in Russia"
            });

            Assert.Equal(count + 1, Context.Cities.Count());
        }

        [Fact]
        public void AddObject()
        {
            var cityService = new Repository<CityDto>(Context);

            var newCity = new CityDto()
            {
                Id = 3,
                Name = "Moscow",
                Description = "City in Russia"
            };
            cityService.Add(newCity);

            var city = Context.Cities.Find(3L);
            Assert.Equal(newCity, city);
        }

        [Fact]
        public void AddSameObjectInstanceTwice()
        {
            var cityService = new Repository<CityDto>(Context);

            var newCity = new CityDto()
            {
                Name = "Moscow",
                Description = "City in Russia"
            };

            var count = Context.Cities.Count();

            cityService.Add(newCity);
            Assert.Throws<ArgumentException>(() => cityService.Add(newCity));
        }

        [Fact]
        public void AddSameObjectsDiffInstancesTwiceDefaultIds()
        {
            var cityService = new Repository<CityDto>(Context);

            var count = Context.Cities.Count();

            cityService.Add(new CityDto()
            {
                Name = "Moscow",
                Description = "City in Russia"
            });

            cityService.Add(new CityDto()
            {
                Name = "Moscow",
                Description = "City in Russia"
            });

            Assert.Equal(count + 2, Context.Cities.Count());
        }

        [Fact]
        public void AddObjectsWithSpecifiedIds()
        {
            var cityService = new Repository<CityDto>(Context);

            var count = Context.Cities.Count();

            cityService.Add(new CityDto()
            {
                Id = 1,
                Name = "Moscow",
                Description = "City in Russia"
            });

            var sameCityId = new CityDto()
            {
                Id = 1,
                Name = "Moscow",
                Description = "City in Russia"
            };

            Assert.Throws<InvalidOperationException>(() => cityService.Add(sameCityId));
        }

        //[Fact]
        //public void AddNonValidObject()
        //{
        //    var salon = new SalonDto()
        //    {
        //        Name = "salon1",
        //        Description = "some des"
        //    };

        //    var service = new Repository<SalonDto>(Context);
        //    service.Add(salon);

        //    // sta s tim???

        //    var obj = Context.Salons.Find(salon.Id);
        //    Assert.NotNull(obj);
        //}

        [Fact]
        public void AddObjectRefersToExistingEntityObject()
        {
            Seed(Context);

            var city = Context.Cities.First();
            var country = Context.Countries.First();

            var salon = new SalonDto()
            {
                Name = "salon1",
                Description = "some des",
                CountryDto = country,
                CityDto = city,
            };

            var service = new Repository<SalonDto>(Context);
            service.Add(salon);

            var obj = Context.Salons.Find(salon.Id);
            Assert.NotNull(obj);
        }

        [Fact]
        public void AddObjectRefersToExistingEntityObjectId()
        {
            Seed(Context);

            var city = Context.Cities.First();
            var country = Context.Countries.First();

            var salon = new SalonDto()
            {
                Name = "salon1",
                Description = "some des",
                CountryId = 1,
                CityId = 1,
            };

            var service = new Repository<SalonDto>(Context);
            service.Add(salon);

            var obj = Context.Salons.Find(salon.Id);
            Assert.NotNull(obj);
        }

        [Fact]
        public void AddRange()
        {
            var cityService = new Repository<CityDto>(Context);

            var cities = new[]
            {
                new CityDto() {
                    Name = "City 1",
                    Description = "Description 1"
                },
                new CityDto() {
                    Name = "City 2",
                    Description = "Description 2"
                }
            };
            cityService.AddRange(cities);

            Assert.Equal(2, Context.Cities.Count());
        }

        [Fact]
        public void RemoveObject()
        {
            Seed(Context);

            var cityService = new Repository<CityDto>(Context);

            var count = Context.Cities.Count();

            CityDto city = Context.Cities.First();
            cityService.Remove(city);

            Assert.Equal(count - 1, Context.Cities.Count());
        }

        [Fact]
        public void RemoveCascadeDeleteShouldFail()
        {
            Seed(Context);

            var city = Context.Cities.First();
            var country = Context.Countries.First();

            var salon = new SalonDto()
            {
                Name = "salon1",
                Description = "some des",
                CountryDto = country,
                CityDto = city,
            };
            Context.Salons.Add(salon);
            Context.SaveChanges();

            var countCities = Context.Cities.Count();
            var countSalons = Context.Salons.Count();
            var service = new Repository<CityDto>(Context);
            Assert.ThrowsAny<Exception>(() => service.Remove(city));
        }

        [Fact]
        public void RemoveItemOnEmptyDb()
        {
            Context.Cities.Add(new CityDto()
            {
                Name = "Zagreb",
                Description = "Capital city of Croatia"
            });
            Context.SaveChanges();

            var cityService = new Repository<CityDto>(Context);

            CityDto city = Context.Cities.First();
            cityService.Remove(city);
            Context.SaveChanges();

            // now DB is empty
            // try to remove once again
            Assert.Throws<DbUpdateConcurrencyException>(() => cityService.Remove(city));
        }

        //[Fact]
        //public void RemoveRange()
        //{
        //    var cities = new[]
        //    {
        //        new CityDto() {
        //            Name = "City 1",
        //            Description = "Description 1"
        //        },
        //        new CityDto() {
        //            Name = "City 2",
        //            Description = "Description 2"
        //        }
        //    };
        //    Context.Set<CityDto>().AddRange(cities);
        //    Context.Set<CityDto>().Add(new CityDto()
        //    {
        //        Name = "City 3",
        //        Description = "Description 3"
        //    });
        //    Context.SaveChanges();

        //    var service = new Repository<CityDto>(Context);

        //    service.RemoveRange(cities);
        //    Assert.Equal(1, Context.Cities.Count());
        //}


        [Fact]
        public void UpdateTest()
        {
            Seed(Context);

            CityDto city = Context.Cities.First();
            string newName = city.Name + "test";

            city.Name = newName;

            var service = new Repository<CityDto>(Context);
            service.Update(city);

            Assert.Equal(city, Context.Cities.Find(city.Id));
        }

        [Fact]
        public void UpdateTestWithNull()
        {
            Seed(Context);

            CityDto city = Context.Cities.First();

            city.Name = null;

            var service = new Repository<CityDto>(Context);
            service.Update(city);

            Assert.Equal(city, Context.Cities.Find(city.Id));
        }


        [Fact]
        public void FindTest()
        {
            Seed(Context);

            var service = new Repository<CityDto>(Context);
            var result = service.Find(p => p.Name.Equals("Zagreb"));

            Assert.NotNull(result);
            var cityDtos = result as CityDto[] ?? result.ToArray();
            Assert.Single(cityDtos);
            
            var noResult = service.Find(p => p.Name == "It's not a name of the city.");
            Assert.NotNull(noResult);
            Assert.Empty(noResult);
        }



        private static void Seed(CarSystemContext context)
        {
            var cities = new[]
            {
                new CityDto ()
                {
                    Name = "Zagreb",
                    Description = "Capital city of Croatia"
                },
                new CityDto()
                {
                    Name = "Paris",
                    Description = "Capital city of France"
                }
            };

            var countries = new[]
            {
                new CountryDto()
                {
                    Name = "HR",
                    Description = "countryHR"
                },
                new CountryDto()
                {
                    Name = "I",
                    Description = "descriptionI"
                }
            };

            context.Countries.AddRange(countries);
            context.SaveChanges();

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }

    }
}
