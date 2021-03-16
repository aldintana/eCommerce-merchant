using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost]
        [Authorize(Roles ="Director")]
        public IActionResult Create()
        {
            try
            {
                _warehouseService.Create();
                return Ok("Good");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }


        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _warehouseService.Get();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
        [Authorize(Roles = "Director, Admin")]
        [HttpGet("{date}", Name = "GetWarehouseByDate")]
        public IActionResult Get(string date=null)
        {
            try
            {
                var result = _warehouseService.Get(date);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
