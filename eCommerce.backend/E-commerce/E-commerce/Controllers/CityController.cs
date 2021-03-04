using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private ICityService _cityService;
        public CityController(ILogger<CityController> logger, ICityService cityService)
        {
            _logger = logger;
            _cityService = cityService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_cityService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest("City not found");
            }
        }

        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_cityService.GetCity(id));
            }
            catch (Exception ex)
            {
                return BadRequest("City not found");
            }
        }
        [HttpPost]
        public IActionResult Create(City city)
        {
            try
            {
                var newCity = _cityService.AddCity(city);
                return CreatedAtRoute("GetById", new { id = newCity.ID }, newCity);
            }
            catch (Exception ex)
            {
                return BadRequest("City not found");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _cityService.DeleteCity(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("City not found");
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody] City city)
        {
            try
            {
                _cityService.EditCity(city);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("City not found");
            }
        }
    }
}
