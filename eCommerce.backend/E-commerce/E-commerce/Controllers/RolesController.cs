using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.EntityModels;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<IdentityRole> roles = _roleManager.Roles.Where(x => x.Name == "Director" || x.Name == "Warehouseman").ToList();
            List<RoleModelVM> roleModel = roles.Select
            (
                x => new RoleModelVM
                {
                    Id = x.Id,
                    Name = x.Name
                }
            ).ToList();
            return Ok(roleModel);
        }
    }
}
