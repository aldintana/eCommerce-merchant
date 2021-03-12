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
using Microsoft.AspNetCore.Authorization;
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
        private IAccountService _accountService;
        private IForgetPasswordLoggerService _forgetPasswordLoggerService;
        public AccountController(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager,
            IEmployeeService employeeService, IEmailSender emailSender, 
            IConfiguration configuration, IAccountService accountService,
            IForgetPasswordLoggerService forgetPasswordLoggerService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _employeeService = employeeService;
            _emailSender = emailSender;
            _configuration = configuration;
            _accountService = accountService;
            _forgetPasswordLoggerService = forgetPasswordLoggerService;
        }
        [HttpPost("Register")]
        [Authorize(Roles ="Admin")]
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

                    await SendPasswordAsync(model.Email, model.Password, model.Email);
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

                //var loginresult = _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
                var roles= await _userManager.GetRolesAsync(user);
                string role="";
                if (roles.Count != 0)
                    role = roles[0];
                if(role=="Customer")
                    return BadRequest("You dont have permission");

                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, role),
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
        //[HttpPost("Logout")]
        //public async Task<IActionResult> LogoutAsync()
        //{            
        //    await _signInManager.SignOutAsync();
        //    return Ok("User logged out");
        //}


        [HttpPost("ResetPassword")]
        [Authorize]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.ResetPasswordAsync(model);

                if (result== "Password has been reset successfully!")
                    return Ok(result);

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var _user = await _userManager.FindByEmailAsync(model.Email);
                if (_user == null)
                    return BadRequest("User doesn't exist");
                if (!_forgetPasswordLoggerService.Add(_user))
                    return BadRequest("Something went wrong");
                await _userManager.RemovePasswordAsync(_user);
                string newPassword = NewPassword(8);
                var result = await _userManager.AddPasswordAsync(_user, newPassword);
                await SendResetPasswordAsync(model.Email, newPassword, model.Email);
                if (result.Succeeded)
                    return Ok("Password has been reset successfully!");
                return BadRequest("Something went wrong");
            }

            return BadRequest("Some properties are not valid");
        }
        private async Task SendPasswordAsync(string email, string password, string username)
        {
            string subject = "Password za korisnicki racun";
            string htmlMessage = @"Poštovani,<br/><br/>" +
                "Pristupni podaci za vaš korisnički račun su:<br/><br/>"+
                "Username: <b>{1}</b> <br/> Password: <b>{0}</b> <br/> <br/> "+ 
                "Molimo Vas da nakon prijave promijenite svoju lozinku." + 
                "<br/><br/>" + "<br/>" + 
                "Ecommerce";
            htmlMessage = string.Format(htmlMessage, password, username);
            await _emailSender.SendEmail(email, subject, htmlMessage);
        }
        private async Task SendResetPasswordAsync(string email, string password, string username)
        {
            string subject = "Password za korisnicki racun";
            string htmlMessage = @"Poštovani,<br/><br/>" +
                "Nakon promjene lozinke, pristupni podaci za vaš korisnički račun su:<br/><br/>" +
                "Username: <b>{1}</b> <br/> Password: <b>{0}</b> <br/> <br/> " +
                "Molimo Vas da nakon prijave promijenite svoju lozinku." +
                "<br/><br/>" + "<br/>" +
                "Ecommerce";
            htmlMessage = string.Format(htmlMessage, password, username);
            await _emailSender.SendEmail(email, subject, htmlMessage);
        }

        private string NewPassword(int length)
        {
            string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowercase = uppercase.ToLower();
            string special = "!@#$%^&*()_+=-";
            string numbers = "0123456789";
            return Random(numbers, 1) + Random(uppercase, 1) + Random(special, 1)
                + Random(lowercase, length - 3);
        }

        private string Random(string text, int length)
        {
            Random random = new Random();
            string result = "";
            for (int i = 0; i < length; i++)
            {
                result += text[random.Next(text.Length)];
            }
            return result;
        }
    }
}
