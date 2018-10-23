//using AutoMapper;
//using Common;
//using DTO.Good;
//using DTO.Grouping;
using AutoMapper;
using Common;
using DTO.LoaderType;
using DTO.PackageType;
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
    public class PackageTypeService : IPackageTypeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<PackageType> _repPackageType;
        public PackageTypeService(IMapper mapper, IUnitOfWork uow, IGenericRepository<PackageType> repPackageType)
        {
            _mapper = mapper;
            _uow = uow;
            _repPackageType = repPackageType;
        }
        /// <summary>
        /// افزودن بارگیر
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        public async Task<PackageTypeDTO> Add(PackageTypeDTO modle)
        {
            try
            {
                var data = _mapper.Map<PackageType>(modle);
                _repPackageType.Insert(data);
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
        public async Task<List<PackageTypeDTO>> GetList()
        {
            try
            {
                List<PackageType> data = await _repPackageType.GetAllAsQueryable().ToListAsync();
                return _mapper.Map<List<PackageTypeDTO>>(data);
            }
            catch (Exception e)
            {
                throw new ServiceExeption(e.Message, e);
            }
        }
    }
}
