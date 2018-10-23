
using DTO.City;
using DTO.LoaderType;
using DTO.Driver;
using DTO.TruckType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    /// <summary>
    /// اینترفیس سرویس گروه بندی ها
    /// </summary>
    public interface ITruckTypeService
    {
        /// <summary>
        /// لیست نوع ماشین
        /// </summary>
        /// <returns></returns>
        Task<List<TruckTypeDTO>> GetList();


        /// <summary>
        /// افزودن نوع ماشین
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        Task<TruckTypeDTO> Add(TruckTypeDTO modle);

    }
}
