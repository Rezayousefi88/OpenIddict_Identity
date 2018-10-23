using AutoMapper;
using DTO.City;
using DTO.Freight;
using DTO.LoaderType;
using DTO.PackageType;
using DTO.Driver;
using DTO.TruckType;
using EntityCode.Entity;
using System.Linq;

namespace Service
{
    class MapperClass : Profile
    {
        public MapperClass()
        {
            CreateMap<Driver, DriverDTO>();
            CreateMap<DriverDTO, Driver>();

            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();

            CreateMap<Freight, FreightDTO>();
            CreateMap<FreightDTO, Freight>();

            CreateMap<Loader, LoaderDTO>();
            CreateMap<LoaderDTO, Loader>();

            CreateMap<TruckType, TruckTypeDTO>();
            CreateMap<TruckTypeDTO, TruckType>();

            CreateMap<PackageType, PackageTypeDTO>();
            CreateMap<PackageTypeDTO, PackageType>();

            //CreateMap<Grouping, GroupingDTO>();


            //CreateMap<GoodAddDTO, Good>();


            //CreateMap<Good, GoodListDTO>()
            //    .ForMember(dto => dto.GroupId, src => src.MapFrom(s => s.GoodGroup.FirstOrDefault().Group.Id))
            //    .ForMember(dto => dto.Group, src => src.MapFrom(s => s.GoodGroup.FirstOrDefault().Group.GroupName))
            ;
        }
    }
}
