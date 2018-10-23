//using AutoMapper;
//using Common;
//using DTO.Good;
//using DTO.Grouping;
using AutoMapper;
using Common;
using DTO.City;
using DTO.Driver;
using EntityCode.Entity;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class CityService : ICityService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<City> _repCity;
        public CityService(IMapper mapper, IUnitOfWork uow, IGenericRepository<City> repCity)
        {
            _mapper = mapper;
            _uow = uow;
            _repCity = repCity;
        }
        /// <summary>
        /// افزودن شهر
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        public async Task<CityDTO> Add(CityDTO modle)
        {
            try
            {
                var data = _mapper.Map<City>(modle);
                _repCity.Insert(data);
                await _uow.SaveChangesAsync();

                modle.ID = data.ID;
                return modle;

            }
            catch (Exception e)
            {
                throw new ServiceExeption(e.Message, e);
            }
        }

        public async Task<List<CityDTO>> GetCityList(int stateID)
        {
            try
            {
                List<City> data = await _repCity.GetAllAsQueryable().Where(c => c.StateCode == stateID).ToListAsync();
                return _mapper.Map<List<CityDTO>>(data);
            }
            catch (Exception e)
            {
                throw new ServiceExeption(e.Message, e);
            }
        }

        /// <summary>
        /// لیست شهرها
        /// </summary>
        /// <returns></returns>
        public async Task<List<CityDTO>> GetStateList()
        {
            try
            {
                List<City> data = await _repCity.GetAllAsQueryable().Where(c => c.StateCode == 0).ToListAsync();
                return _mapper.Map<List<CityDTO>>(data);
            }
            catch (Exception e)
            {
                throw new ServiceExeption(e.Message, e);
            }
        }


        /// <summary>
        /// لیست شهرها
        /// </summary>
        /// <returns></returns>
        public async Task<List<CityDTO>> GetFilterStateList(String StateName)
        {
            try
            {
                if (StateName == null)
                {
                    List<City> data = await _repCity.GetAllAsQueryable().Where(c => c.StateCode == 0).ToListAsync();
                    return _mapper.Map<List<CityDTO>>(data);
                }
                else
                {
                    List<City> data = await _repCity.GetAllAsQueryable().Where(c => c.StateCode == 0 && c.CityName.Contains(StateName)).ToListAsync();
                    return _mapper.Map<List<CityDTO>>(data);
                }
            }
            catch (Exception e)
            {
                throw new ServiceExeption(e.Message, e);
            }
        }

    }
}
