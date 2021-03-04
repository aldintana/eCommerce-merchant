using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GenderSubCategoryController : ControllerBase
    {
        private readonly ILogger<GenderSubCategoryController> _logger;
        private IGenderSubCategoryService _genderSubCategoryService;
        public GenderSubCategoryController(ILogger<GenderSubCategoryController> logger, 
            IGenderSubCategoryService genderSubCategoryService)
        {
            _logger = logger;
            _genderSubCategoryService = genderSubCategoryService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_genderSubCategoryService.GetAll());
            }
            catch (Exception)
            {
                return BadRequest("GenderSubCategory not found");
            }
        }
    }
}
