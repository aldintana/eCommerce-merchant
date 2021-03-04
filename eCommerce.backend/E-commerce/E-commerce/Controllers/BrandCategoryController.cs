using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Data.EntityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class BrandCategoryController : ControllerBase
    {
        private readonly ILogger<BrandCategoryController> _logger;
        private IBrandCategoryService _brandCategoryService;
        public BrandCategoryController(ILogger<BrandCategoryController> logger, IBrandCategoryService brandCategoryService)
        {
            _logger = logger;
            _brandCategoryService = brandCategoryService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_brandCategoryService.GetAll());
            }
            catch (Exception)
            {
                return BadRequest("Brand not found");
            }
        }

        [HttpGet("{id}", Name = "GetBrandById")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_brandCategoryService.GetBrandCategory(id));
            }
            catch (Exception)
            {
                return BadRequest("Brand not found");
            }
        }
        [HttpPost]
        public IActionResult Create(BrandCategory brandCategory)
        {
            try
            {
                var newBrand = _brandCategoryService.AddBrandCategory(brandCategory);
                return CreatedAtRoute("GetBrandById", new { id = newBrand.ID }, newBrand);
            }
            catch (Exception)
            {
                return BadRequest("Brand not found");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _brandCategoryService.DeleteBrandCategory(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Brand not found");
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody] BrandCategory brandCategory)
        {
            try
            {
                _brandCategoryService.EditBrandCategory(brandCategory);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Brand not found");
            }
        }
    }
}
