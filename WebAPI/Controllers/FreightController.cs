using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using DTO.Freight;
using DTO.Driver;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FreightController : ControllerBase
    {
        private readonly IFreightService _service;
        public FreightController(IFreightService service)
        {
            _service = service;
        }
        /// <summary>
        /// دریافت لیست بارها
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] FreightDTO model)
        {
            if (!ModelState.IsValid)
            {
                return new ValidationFailedResult(ModelState);
            }
            try
            {
                var data = await _service.Add(model);
                return Ok(data);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PopUpError", e.Message);
                return new ValidationFailedResult(ModelState);
            }
        }

        /// <summary>
        /// دریافت لیستی از بارها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var data = await _service.GetFreightListAsync();
                return Ok(data);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PopUpError", e.Message);
                return new ValidationFailedResult(ModelState);
            }

        }



        /// <summary>
        /// دریافت لیستی از بارها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFilterList(int SourceStateId , int DestinationStateId , string LoadDate , string ExpireDate , int Weight)
        {
            try
            {
                FilterFreightDTO filter = new FilterFreightDTO();
                filter.SourceCityId = SourceStateId;
                filter.DestinationCityId = DestinationStateId;
                filter.LoadDate = LoadDate;
                filter.ExpireDate = ExpireDate;
                filter.Weight = Weight;
                var data = await _service.GetFreightListAsync(filter);
                return Ok(data);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PopUpError", e.Message);
                return new ValidationFailedResult(ModelState);
            }

        }


    }
}
