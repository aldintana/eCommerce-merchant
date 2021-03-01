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
            return Ok(_categoryService.GetAll());
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public IActionResult GetById(int id)
        {
            return Ok(_categoryService.GetCategory(id));
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            var newCategory = _categoryService.AddCategory(category);
            return CreatedAtRoute("GetCategoryById", new { id = newCategory.ID }, newCategory);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.DeleteCategory(id);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update([FromBody] Category category)
        {
            _categoryService.EditCategory(category);
            return Ok();
        }
    }
}
