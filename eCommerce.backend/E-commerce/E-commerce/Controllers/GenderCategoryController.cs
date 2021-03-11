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
            try
            {
                return Ok(_genderCategoryService.GetAll());
            }
            catch (Exception)
            {
                return BadRequest("Gender not found");
            }
        }

        [HttpGet("{id}", Name = "GetGenderById")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_genderCategoryService.GetGenderCategory(id));
            }
            catch (Exception)
            {
                return BadRequest("Gender not found");
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(GenderCategory genderCategory)
        {
            try
            {
                var newGender = _genderCategoryService.AddGenderCategory(genderCategory);
                return CreatedAtRoute("GetGenderById", new { id = newGender.ID }, newGender);
            }
            catch (Exception)
            {
                return BadRequest("Value is null");
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _genderCategoryService.DeleteGenderCategory(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Gender not found");
            }
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromBody] GenderCategory genderCategory)
        {
            try
            {
                _genderCategoryService.EditGenderCategory(genderCategory);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Gender not found");
            }
        }
    }
}
