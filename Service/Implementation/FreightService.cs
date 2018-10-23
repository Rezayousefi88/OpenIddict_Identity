//using AutoMapper;
//using Common;
//using DTO.Good;
//using DTO.Grouping;
using AutoMapper;
using Common;
using DTO.Freight;
using EntityCode.Entity;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class FreightService : IFreightService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Freight> _repFreight;
        private readonly IGenericRepository<City> _repCity;
        private readonly IGenericRepository<Loader> _repLoader;
        private readonly IGenericRepository<TruckType> _repTruckType;
        private readonly IGenericRepository<PackageType> _repPackageType;
        public FreightService(
            IMapper mapper,
            IUnitOfWork uow,
            IGenericRepository<Freight> repFreight ,
            IGenericRepository<City> repCity,
            IGenericRepository<Loader> repLoader,
            IGenericRepository<TruckType> repTruckType,
            IGenericRepository<PackageType> repPackageType
            )
        {
            _mapper = mapper;
            _uow = uow;
            _repFreight = repFreight;
            _repCity = repCity;
            _repLoader = repLoader;
            _repTruckType = repTruckType;
            _repPackageType = repPackageType;
        }
        /// <summary>
        /// افزودن بار
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        public async Task<FreightDTO> Add(FreightDTO modle)
        {
            try
            {
                var data = _mapper.Map<Freight>(modle);
                _repFreight.Insert(data);
                await _uow.SaveChangesAsync();

                modle.ID = data.ID;
                return modle;

            }
            catch (Exception e)
            {
                throw new ServiceExeption(e.Message, e);
            }
        }


        public async Task<List<FreightDTO>> GetFreightListAsync()
        {
            try
            {
                List<Freight> data = await _repFreight.GetAllAsQueryable().ToListAsync();

                List<FreightDTO> datas = new List<FreightDTO>();
                
                foreach (var item in data)
                {
                    FreightDTO freightDTO = new FreightDTO();
                    freightDTO.ID = item.ID;
                    freightDTO.BaseTruckRent = item.BaseTruckRent;
                    freightDTO.SourceCityId = item.SourceCityId;

                    City city = _repCity.GetAllAsQueryable().Where(c => c.Code == item.SourceCityId).FirstOrDefault();

                    freightDTO.SourceCityName = city.CityName;
                    freightDTO.DestinationCityId = item.DestinationCityId;

                    City cityDes = _repCity.GetAllAsQueryable().Where(c => c.Code == item.DestinationCityId).FirstOrDefault();

                    freightDTO.DestinationCityName = cityDes.CityName;
                    freightDTO.CreateDate = item.CreateDate;
                    freightDTO.LoadDate = item.LoadDate;
                    freightDTO.LoadTime = item.LoadTime;
                    freightDTO.ExpireDate = item.ExpireDate;
                    freightDTO.ExpireTime = item.ExpireTime;
                    freightDTO.PackageType = item.PackageType;
                    freightDTO.GoodName = item.GoodName;

                    PackageType packageType = _repPackageType.GetAllAsQueryable().Where(c => c.ID == item.PackageType).FirstOrDefault();

                    freightDTO.PackageName = packageType.Name;
                    freightDTO.TruckType = item.TruckType;

                    TruckType truckType = _repTruckType.GetAllAsQueryable().Where(c => c.ID == item.TruckType).FirstOrDefault();

                    freightDTO.TruckName = truckType.Name;
                    freightDTO.LoaderType = item.LoaderType;

                    Loader loader = _repLoader.GetAllAsQueryable().Where(c => c.ID == item.LoaderType).FirstOrDefault();

                    freightDTO.loaderName = loader.Name;
                    freightDTO.Weight = item.Weight;
                    freightDTO.Description = item.Description;
                    freightDTO.Tell = item.Tell;
                    freightDTO.BaseTruckRent = item.BaseTruckRent;
                    freightDTO.BaseTruckRent = item.BaseTruckRent;
                    datas.Add(freightDTO);
                }

                return datas;
            }
            catch (Exception e)
            {
                throw new ServiceExeption(e.Message, e);
            }
        }

        public async Task<List<FreightDTO>> GetFreightListAsync(FilterFreightDTO  filter)
        {
            try
            {
                var query = _repFreight.GetAllAsQueryable();
                if (filter.DestinationCityId > 0)
                {
                    int IntegerDestination = (filter.DestinationCityId) / 1000000;
                    query = query.Where(c => (c.DestinationCityId) / 1000000 == IntegerDestination);
                }

                if (filter.SourceCityId > 0)
                {
                    int IntegerSource = (filter.SourceCityId) / 1000000;
                    query = query.Where(c => (c.SourceCityId)/1000000 == IntegerSource);
                }

                if (filter.Weight >= 3)
                {
                    query = query.Where(c => c.Weight >= 3);
                }
                else if(filter.Weight < 3 && filter.Weight > 1)
                {
                    query = query.Where(c => c.Weight < 3);
                }

                if (filter.LoadDate != null)
                {
                    query = query.Where(c => string.Compare(c.LoadDate , filter.LoadDate) >= 0);
                    
                }

                if (filter.ExpireDate != null)
                {
                    query = query.Where(c => string.Compare(c.ExpireDate, filter.ExpireDate) <= 0);

                }

                List<Freight> data = await query.ToListAsync();

                List <FreightDTO> datas = new List<FreightDTO>();

                foreach (var item in data)
                {
                    FreightDTO freightDTO = new FreightDTO();
                    freightDTO.ID = item.ID;
                    freightDTO.BaseTruckRent = item.BaseTruckRent;
                    freightDTO.SourceCityId = item.SourceCityId;

                    City city = _repCity.GetAllAsQueryable().Where(c => c.Code == item.SourceCityId).FirstOrDefault();

                    freightDTO.SourceCityName = city.CityName;
                    freightDTO.DestinationCityId = item.DestinationCityId;

                    City cityDes = _repCity.GetAllAsQueryable().Where(c => c.Code == item.DestinationCityId).FirstOrDefault();

                    freightDTO.DestinationCityName = cityDes.CityName;
                    freightDTO.CreateDate = item.CreateDate;
                    freightDTO.LoadDate = item.LoadDate;
                    freightDTO.LoadTime = item.LoadTime;
                    freightDTO.ExpireDate = item.ExpireDate;
                    freightDTO.ExpireTime = item.ExpireTime;
                    freightDTO.PackageType = item.PackageType;
                    freightDTO.GoodName = item.GoodName;

                    PackageType packageType = _repPackageType.GetAllAsQueryable().Where(c => c.ID == item.PackageType).FirstOrDefault();

                    freightDTO.PackageName = packageType.Name;
                    freightDTO.TruckType = item.TruckType;

                    TruckType truckType = _repTruckType.GetAllAsQueryable().Where(c => c.ID == item.TruckType).FirstOrDefault();

                    freightDTO.TruckName = truckType.Name;
                    freightDTO.LoaderType = item.LoaderType;

                    Loader loader = _repLoader.GetAllAsQueryable().Where(c => c.ID == item.LoaderType).FirstOrDefault();

                    freightDTO.loaderName = loader.Name;
                    freightDTO.Weight = item.Weight;
                    freightDTO.Description = item.Description;
                    freightDTO.Tell = item.Tell;
                    freightDTO.BaseTruckRent = item.BaseTruckRent;
                    freightDTO.BaseTruckRent = item.BaseTruckRent;
                    datas.Add(freightDTO);
                }

                return datas;
            }
            catch (Exception e)
            {
                throw new ServiceExeption(e.Message, e);
            }
        }

    }
}
