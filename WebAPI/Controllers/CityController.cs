using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using DTO.City;
using DTO.Driver;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _service;
        public CityController(ICityService service)
        {
            _service = service;
        }
        /// <summary>
        ///  اضافه کردن شهر یا استان
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CityDTO model)
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
        /// دریافت لیستی از استان ها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetStateList()
        {
            try
            {
                var data = await _service.GetStateList();
                return Ok(data);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PopUpError", e.Message);
                return new ValidationFailedResult(ModelState);
            }

        }

        /// <summary>
        /// دریافت لیستی از استان ها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetFilterStateList(String StateName)
        {
            try
            {
                var data = await _service.GetFilterStateList(StateName);
                return Ok(data);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("PopUpError", e.Message);
                return new ValidationFailedResult(ModelState);
            }

        }

        /// <summary>
        /// دریافت لیستی از شهر ها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCityList(int stateID)
        {
            try
            {
                var data = await _service.GetCityList(stateID);
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
