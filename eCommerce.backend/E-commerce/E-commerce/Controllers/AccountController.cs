using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Data.EntityModels;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<Account> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IEmployeeService _employeeService;
        public AccountController(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,
            IEmployeeService employeeService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _employeeService = employeeService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterVM model)
        {
            if (model == null)
                throw new NullReferenceException("Register model is null");
            if (ModelState.IsValid)
            {
                var role = _roleManager.FindByIdAsync(model.RoleId).Result;
                if(role.Name!="Warehouseman"||role.Name!="Director")
                    throw new NullReferenceException("Role is not good");
                var user = new Account
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, "Sifra54321!!!!!");
                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, role.Name);

                    if(!roleResult.Succeeded)
                        throw new NullReferenceException("Role is not good");
                    Employee employee = new Employee
                    {
                        Account = user,
                        BranchID = model.BranchId
                    };
                    _employeeService.AddEmployee(employee);
                    return Ok(result); // Status Code: 200 
                }
                    

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }
    }
}
