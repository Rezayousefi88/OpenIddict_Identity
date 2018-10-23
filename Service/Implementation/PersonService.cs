//using AutoMapper;
//using Common;
//using DTO.Good;
//using DTO.Grouping;
using AutoMapper;
using Common;
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
    public class PersonService : IPersonService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly IGenericRepository<Driver> _repPerson;
        public PersonService(IMapper mapper, IUnitOfWork uow, IGenericRepository<Driver> repPerson)
        {
            _mapper = mapper;
            _uow = uow;
            _repPerson = repPerson;
        }
        /// <summary>
        /// افزودن افراد
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        public async Task<DriverDTO> Add(DriverDTO modle)
        {
            try
            {
                var data = _mapper.Map<Driver>(modle);
                _repPerson.Insert(data);
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
        /// لیست افراد
        /// </summary>
        /// <returns></returns>
        public async Task<List<DriverDTO>> GetList()
        {
            try
            {
                List<Driver> data = await _repPerson.GetAllAsQueryable().Where(x=>x.Age==10).ToListAsync();
                return _mapper.Map<List<DriverDTO>>(data);
            }
            catch (Exception e)
            {
                throw new ServiceExeption(e.Message, e);
            }
        }

        //        public GoodService(IMapper mapper, IUnitOfWork uow
        //            , IGenericRepository<Good> mainService
        //            , IPropertiesService propertic
        //            , IGroupingService group
        //            , IGoodGroupService goodGroup
        //            , IPropertyGoodService property
        //            )
        //        {
        //            _mapper = mapper;
        //            _uow = uow;
        //            _mainService = mainService;
        //            _propertic = propertic;
        //            _group = group;
        //            _property = property;
        //            _goodGroup = goodGroup;
        //        }

        //        public async Task<bool> Add(GoodAddDTO model)
        //        {
        //            try
        //            {
        //                bool Group = await _group.IsExists(model.GroupId);
        //                bool Propertic = true;
        //                foreach (var item in model.dataItems)
        //                {
        //                    if (Propertic)
        //                    {
        //                        Propertic = await _propertic.IsExists(item.Id);
        //                    }
        //                }

        //                if (Group && Propertic)
        //                {
        //                    using (var transaction = _uow.TransactionalSave())
        //                    {
        //                        var data = _mapper.Map<Good>(model);
        //                        _mainService.Insert(data);
        //                        await _uow.SaveChangesAsync();

        //                        var temp = new GoodGroup() { GoodId = data.Id, GroupId = model.GroupId };
        //                        await _goodGroup.Add(temp);

        //                        foreach (var item in model.dataItems)
        //                        {
        //                            await _property.AddEdit( new PropertyGood() { GoodId = data.Id , PropertyId = item.Id , PercentGood = item.PercentGood} );
        //                        }

        //                        transaction.Commit();
        //                        return true;
        //                    }
        //                }
        //                else
        //                {
        //                    throw new Exception("اطلاعات وارد شده صحیح نمی باشد");
        //                }


        //            }
        //            catch (Exception e)
        //            {
        //                throw new ServiceExeption(e.Message, e);
        //            }

        //        }

        //        public async Task<List<GoodListDTO>> GetStateList()
        //        {
        //            try
        //            {
        //                var data = await _mainService.GetAllAsQueryable()
        //                    .Include(x=>x.GoodGroup)
        //                        .ThenInclude(x=>x.Group)
        //                    .ToListAsync();

        //                var dataObject = _mapper.Map<List<GoodListDTO>>(data);
        //                return dataObject;
        //            }
        //            catch (Exception e)
        //            {
        //                throw new ServiceExeption(e.Message, e);
        //            }

        //        }
    }
}
