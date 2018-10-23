
using DTO.City;
using DTO.Freight;
using DTO.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    /// <summary>
    /// اینترفیس سرویس گروه بندی ها
    /// </summary>
    public interface IFreightService
    {
        /// <summary>
        /// لیست بارها
        /// </summary>
        /// <returns></returns>
        Task<List<FreightDTO>> GetFreightListAsync(FilterFreightDTO filter);

        /// <summary>
        /// لیست بارها
        /// </summary>
        /// <returns></returns>
        Task<List<FreightDTO>> GetFreightListAsync();


        /// <summary>
        /// افزودن بار
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        Task<FreightDTO> Add(FreightDTO modle);
    }
}
