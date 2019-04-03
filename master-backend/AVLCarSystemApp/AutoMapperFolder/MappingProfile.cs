using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AVLCarSystemApp.ModelsBL;
using AVLCarSystemApp.ModelsDTO;

namespace AVLCarSystemApp.AutoMapperFolder
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<CityDto, City>();
            CreateMap<City,CityDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Country, CountryDto>();
            CreateMap<EngineDto, Engine>();
            CreateMap<Engine, EngineDto>();
            CreateMap<EngineTypeDto, EngineType>();
            CreateMap<EngineType, EngineTypeDto>();
            CreateMap<EquipmentDto, Equipment>();
            CreateMap<Equipment, EquipmentDto>();
            CreateMap<InventoryDto, Inventory>();
            CreateMap<Inventory, InventoryDto>();
            CreateMap<ManufacturerDto, Manufacturer>();
            CreateMap<Manufacturer, ManufacturerDto>();
            CreateMap<ModelDto, Model>();
            CreateMap<Model, ModelDto>();
            CreateMap<SalonDto, Salon>();
            CreateMap<Salon, SalonDto>();         

        }
    }
}
