using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private IInventoryService _inventoryService;
        public InventoryController(ILogger<InventoryController> logger,
            IInventoryService inventoryService)
        {
            _logger = logger;
            _inventoryService = inventoryService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                return Ok(_inventoryService.Get());
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
            
        }


        [HttpGet("{name}", Name = "GetInventoryByName")]
        [Authorize]
        public IActionResult GetByName(string name = null)
        {
            try
            {
                return Ok(_inventoryService.Get(name));
            }
            catch (Exception ex)
            {
                return BadRequest("Branch not found");
            }
        }
    }
}
