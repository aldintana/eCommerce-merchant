using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Create(CouponAddVM coupon)
        {
            try
            {
                _couponService.Create(coupon);
                return Ok("Good");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
            
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result=_couponService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
           
        }
    }
}
