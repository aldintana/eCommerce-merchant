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
    public class SubCategoryController : ControllerBase
    {
        private readonly ILogger<SubCategoryController> _logger;
        private ISubCategoryService _subCategoryService;
        public SubCategoryController(ILogger<SubCategoryController> logger, ISubCategoryService subCategoryService)
        {
            _logger = logger;
            _subCategoryService = subCategoryService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_subCategoryService.GetAll());
        }

        [HttpGet("{id}", Name = "GetSubCategoryById")]
        public IActionResult GetById(int id)
        {
            return Ok(_subCategoryService.GetSubCategory(id));
        }
        [HttpPost]
        public IActionResult Create(SubCategory subCategory)
        {
            var newSubCategory = _subCategoryService.AddSubCategory(subCategory);
            return CreatedAtRoute("GetSubCategoryById", new { id = newSubCategory.ID }, newSubCategory);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _subCategoryService.DeleteSubCategory(id);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update([FromBody] SubCategory subCategory)
        {
            _subCategoryService.EditSubCategory(subCategory);
            return Ok();
        }
    }
}
