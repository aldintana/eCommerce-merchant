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
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private ICategoryService _categoryService;
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_categoryService.GetAll());
            }
            catch (Exception)
            {
                return BadRequest("Category not found");
            }
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_categoryService.GetCategory(id));
            }
            catch (Exception)
            {
                return BadRequest("Category not found");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Category category)
        {
            try
            {
                var newCategory = _categoryService.AddCategory(category);
                return CreatedAtRoute("GetCategoryById", new { id = newCategory.ID }, newCategory);
            }
            catch (Exception)
            {
                return BadRequest("Category not found");
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Category not found");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromBody] Category category)
        {
            try
            {
                _categoryService.EditCategory(category);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Category not found");
            }
        }
    }
}
