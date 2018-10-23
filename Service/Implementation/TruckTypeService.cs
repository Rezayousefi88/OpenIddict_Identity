//using AutoMapper;
//using Common;
//using DTO.Good;
//using DTO.Grouping;
using AutoMapper;
using Common;
using DTO.LoaderType;
using DTO.Driver;
using DTO.TruckType;
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
    public class TruckTypeService : ITruckTypeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<TruckType> _repTruckType;
        public TruckTypeService(IMapper mapper, IUnitOfWork uow, IGenericRepository<TruckType> repTruckType)
        {
            _mapper = mapper;
            _uow = uow;
            _repTruckType = repTruckType;
        }
        /// <summary>
        /// افزودن بارگیر
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        public async Task<TruckTypeDTO> Add(TruckTypeDTO modle)
        {
            try
            {
                var data = _mapper.Map<TruckType>(modle);
                _repTruckType.Insert(data);
                await _uow.SaveChangesAsync();

                modle.ID = data.ID;
                return modle;

            }
            catch (Exception e)
            {
                throw new ServiceExeption(e.Message, e);
            }
        }
        /// <summary>
        /// لیست بارگیر
        /// </summary>
        /// <returns></returns>
        public async Task<List<TruckTypeDTO>> GetList()
        {
            try
            {
                List<TruckType> data = await _repTruckType.GetAllAsQueryable().ToListAsync();
                return _mapper.Map<List<TruckTypeDTO>>(data);
            }
            catch (Exception e)
            {
                throw new ServiceExeption(e.Message, e);
            }
        }
    }
}
