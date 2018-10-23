//using AutoMapper;
//using Common;
//using DTO.Good;
//using DTO.Grouping;
using AutoMapper;
using Common;
using DTO.LoaderType;
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
    public class LoaderService : ILoaderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Loader> _repLoader;
        public LoaderService(IMapper mapper, IUnitOfWork uow, IGenericRepository<Loader> repLoader)
        {
            _mapper = mapper;
            _uow = uow;
            _repLoader = repLoader;
        }
        /// <summary>
        /// افزودن بارگیر
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        public async Task<LoaderDTO> Add(LoaderDTO modle)
        {
            try
            {
                var data = _mapper.Map<Loader>(modle);
                _repLoader.Insert(data);
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
        public async Task<List<LoaderDTO>> GetList()
        {
            try
            {
                List<Loader> data = await _repLoader.GetAllAsQueryable().ToListAsync();
                return _mapper.Map<List<LoaderDTO>>(data);
            }
            catch (Exception e)
            {
                throw new ServiceExeption(e.Message, e);
            }
        }
    }
}
