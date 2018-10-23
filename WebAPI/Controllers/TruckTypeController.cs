﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using DTO.Freight;
using DTO.PackageType;
using DTO.Driver;
using DTO.TruckType;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TruckTypeController : ControllerBase
    {
        private readonly ITruckTypeService _service;
        public TruckTypeController(ITruckTypeService service)
        {
            _service = service;
        }
        /// <summary>
        /// اضافه کردن نوع بسته
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TruckTypeDTO model)
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
        /// دریافت لیستی از نوع بسته
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var data = await _service.GetList();
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
