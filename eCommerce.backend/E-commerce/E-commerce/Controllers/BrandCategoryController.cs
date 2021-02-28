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
            return Ok(_brandCategoryService.GetAll());
        }

        [HttpGet("{id}", Name = "GetBrandById")]
        public IActionResult GetById(int id)
        {
            return Ok(_brandCategoryService.GetBrandCategory(id));
        }
        [HttpPost]
        public IActionResult Create(BrandCategory brandCategory)
        {
            var newBrand = _brandCategoryService.AddBrandCategory(brandCategory);
            return CreatedAtRoute("GetBrandById", new { id = newBrand.BrandCategoryID }, newBrand);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _brandCategoryService.DeleteBrandCategory(id);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update([FromBody] BrandCategory brandCategory)
        {
            _brandCategoryService.EditBrandCategory(brandCategory);
            return Ok();
        }
    }
}
