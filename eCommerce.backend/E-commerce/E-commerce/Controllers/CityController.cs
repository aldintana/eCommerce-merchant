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
            return Ok(_cityService.GetAll());
        }

        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            return Ok(_cityService.GetCity(id));
        }
        [HttpPost]
        public IActionResult Create(City city)
        {
            var newCity = _cityService.AddCity(city);
            return CreatedAtRoute("GetById", new { id = newCity.CityID }, newCity);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _cityService.DeleteCity(id);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update([FromBody] City city)
        {
            _cityService.EditCity(city);
            return Ok();
        }
    }
}
