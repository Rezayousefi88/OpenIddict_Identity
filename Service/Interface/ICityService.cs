
using DTO.City;
using DTO.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    /// <summary>
    /// اینترفیس سرویس گروه بندی ها
    /// </summary>
    public interface ICityService
    {
        /// <summary>
        /// لیست استان
        /// </summary>
        /// <returns></returns>
        Task<List<CityDTO>> GetStateList();

        Task<List<CityDTO>> GetFilterStateList(string StateName);


        /// <summary>
        /// افزودن شهر
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        Task<CityDTO> Add(CityDTO modle);


        /// <summary>
        /// لیست شهر
        /// </summary>
        /// <returns></returns>
        Task<List<CityDTO>> GetCityList(int stateID);
    }
}
