
using DTO.City;
using DTO.LoaderType;
using DTO.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    /// <summary>
    /// اینترفیس سرویس گروه بندی ها
    /// </summary>
    public interface ILoaderService
    {
        /// <summary>
        /// لیست بارگیرها
        /// </summary>
        /// <returns></returns>
        Task<List<LoaderDTO>> GetList();


        /// <summary>
        /// افزودن بارگیر
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        Task<LoaderDTO> Add(LoaderDTO modle);

    }
}
