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
            return Ok(_sizeService.GetAll());
        }

        [HttpGet("{id}", Name = "GetSizeById")]
        public IActionResult GetById(int id)
        {
            return Ok(_sizeService.GetSize(id));
        }
        [HttpPost]
        public IActionResult Create(Size size)
        {
            var newSize = _sizeService.AddSize(size);
            return CreatedAtRoute("GetSizeById", new { id = newSize.ID }, newSize);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _sizeService.DeleteSize(id);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update([FromBody] Size size)
        {
            _sizeService.EditSize(size);
            return Ok();
        }
    }
}
