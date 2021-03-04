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
            try
            {
                return Ok(_subCategoryService.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest("Subcategory not found");
            }
        }

        [HttpGet("{id}", Name = "GetSubCategoryById")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_subCategoryService.GetSubCategory(id));
            }
            catch (Exception)
            {

                return BadRequest("Subcategory not found");
            }
        }
        [HttpPost]
        public IActionResult Create(SubCategory subCategory)
        {
            try
            {
                var newSubCategory = _subCategoryService.AddSubCategory(subCategory);
                return CreatedAtRoute("GetSubCategoryById", new { id = newSubCategory.ID }, newSubCategory);
            }
            catch (Exception)
            {
                return BadRequest("Value is null");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _subCategoryService.DeleteSubCategory(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Subcategory not found");
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody] SubCategory subCategory)
        {
            try
            {
                _subCategoryService.EditSubCategory(subCategory);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Subcategory not found");
            }
        }
    }
}
