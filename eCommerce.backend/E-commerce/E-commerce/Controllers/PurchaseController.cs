using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace E_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly ILogger<PurchaseController> _logger;
        private IPurchaseService _purchaseService;
        public PurchaseController(ILogger<PurchaseController> logger, 
            IPurchaseService purchaseService)
        {
            _logger = logger;
            _purchaseService = purchaseService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(PurchaseAddVM purchase)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (purchase.Quantity < 1)
                        return BadRequest("Something went wrong");
                    var result = _purchaseService.Add(purchase);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
            return BadRequest("Something went wrong");
        }
    }
}
