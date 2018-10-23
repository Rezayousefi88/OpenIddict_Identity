
using DTO.City;
using DTO.PackageType;
using DTO.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    /// <summary>
    /// اینترفیس سرویس گروه بندی ها
    /// </summary>
    public interface IPackageTypeService
    {
        /// <summary>
        /// لیست نوع بسته
        /// </summary>
        /// <returns></returns>
        Task<List<PackageTypeDTO>> GetList();


        /// <summary>
        /// افزودن نوع بسته
        /// </summary>
        /// <param name="modle"></param>
        /// <returns></returns>
        Task<PackageTypeDTO> Add(PackageTypeDTO modle);

    }
}
