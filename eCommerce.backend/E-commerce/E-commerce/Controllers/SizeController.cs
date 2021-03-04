using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Data.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        private readonly ILogger<SizeController> _logger;
        private ISizeService _sizeService;
        public SizeController(ILogger<SizeController> logger, ISizeService sizeService)
        {
            _logger = logger;
            _sizeService = sizeService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_sizeService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest("Size not found");
            }
        }

        [HttpGet("{id}", Name = "GetSizeById")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_sizeService.GetSize(id));
            }
            catch (Exception ex)
            {

                return BadRequest("Size not found");
            }
        }
        [HttpPost]
        public IActionResult Create(Size size)
        {
            try
            {
                var newSize = _sizeService.AddSize(size);
                return CreatedAtRoute("GetSizeById", new { id = newSize.ID }, newSize);
            }
            catch (Exception ex)
            {
                return BadRequest("Size is null");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _sizeService.DeleteSize(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Size not found");
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody] Size size)
        {
            try
            {
                _sizeService.EditSize(size);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Size not found");
            }
        }
    }
}
