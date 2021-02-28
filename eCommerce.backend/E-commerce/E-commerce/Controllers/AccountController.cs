using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Data.EntityModels;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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
        private IConfiguration _configuration;
        public AccountController(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,
            IEmployeeService employeeService, IEmailSender emailSender, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _employeeService = employeeService;
            _emailSender = emailSender;
            _configuration = configuration;
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

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginVM model)
        {
            if (model == null)
                throw new NullReferenceException("Login model is null");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if(user==null)
                    return BadRequest("User doesn't exist");

                var result = await _userManager.CheckPasswordAsync(user, model.Password);

                if(!result)
                    return BadRequest("Password is not correct");

                var roles= await _userManager.GetRolesAsync(user);
                string role="";
                if (roles.Count != 0)
                    role = roles[0];
                var claims = new[]
                {
                    new Claim("Email", model.Email),
                    new Claim("Id", user.Id),
                    new Claim("Role", role)                 
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

                var token = new JwtSecurityToken
                    (
                        issuer: _configuration["AuthSettings:Issuer"],
                        audience: _configuration["AuthSettings:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddDays(10),
                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                    );

                string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(tokenAsString);
            }

            return BadRequest("Some properties are not valid"); 
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
