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
    public class GenderCategoryController : ControllerBase
    {
        private readonly ILogger<GenderCategoryController> _logger;
        private IGenderCategoryService _genderCategoryService;
        public GenderCategoryController(ILogger<GenderCategoryController> logger, IGenderCategoryService genderCategoryService)
        {
            _logger = logger;
            _genderCategoryService = genderCategoryService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_genderCategoryService.GetAll());
        }

        [HttpGet("{id}", Name = "GetGenderById")]
        public IActionResult GetById(int id)
        {
            return Ok(_genderCategoryService.GetGenderCategory(id));
        }
        [HttpPost]
        public IActionResult Create(GenderCategory genderCategory)
        {
            var newGender = _genderCategoryService.AddGenderCategory(genderCategory);
            return CreatedAtRoute("GetGenderById", new { id = newGender.ID }, newGender);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _genderCategoryService.DeleteGenderCategory(id);
            return Ok();
        }
        [HttpPut]
        public IActionResult Update([FromBody] GenderCategory genderCategory)
        {
            _genderCategoryService.EditGenderCategory(genderCategory);
            return Ok();
        }
    }
}
