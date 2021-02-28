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
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,
            IEmployeeService employeeService, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _employeeService = employeeService;
            _emailSender = emailSender;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterVM model)
        {
            if (model == null)
                throw new NullReferenceException("Register model is null");
            if (ModelState.IsValid)
            {
                var role = _roleManager.FindByIdAsync(model.RoleId).Result;
                if(role.Name=="Customer"||role.Name=="Admin")
                    throw new NullReferenceException("Role is not good");
                var user = new Account
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, role.Name);

                    if(!roleResult.Succeeded)
                        throw new NullReferenceException("Role is not good");

                    await SendPasswordAsync(model.Email, model.Password);
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

        private async Task SendPasswordAsync(string email, string lozinka)
        {
            string subject = "Password za korisnicki racun";
            string htmlMessage = @"Poštovani,<br/><br/>" + "Password za Vaš korisnički račun je: <b>{0}</b><br/>" + "Molimo Vas da nakon prijave promijenite svoju lozinku." + "<br/><br/>" + "<br/>" + "Ecommerce";
            htmlMessage = string.Format(htmlMessage, lozinka);
            await _emailSender.SendEmail(email, subject, htmlMessage);
        }
    }
}
