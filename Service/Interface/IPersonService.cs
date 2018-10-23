
using DTO.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    /// <summary>
    /// اینترفیس سرویس گروه بندی ها
    /// </summary>
    public interface IPersonService
    {
        /// <summary>
        /// لیست افراد
        /// </summary>
        /// <returns></returns>
        Task<List<DriverDTO>> GetList();
        /// <summary>
        /// افزودن افراد
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        Task<DriverDTO> Add(DriverDTO modle);
    }
}
