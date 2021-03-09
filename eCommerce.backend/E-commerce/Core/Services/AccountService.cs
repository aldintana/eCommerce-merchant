using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using Data.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        private E_commerceDB _context;
        private UserManager<Account> _userManager;
        private Account _user;
        public AccountService(E_commerceDB context,
            UserManager<Account> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            if(httpContextAccessor.HttpContext.User.Identity.Name!=null)
            {
                _user = _context.Account.First(x => x.UserName == httpContextAccessor.HttpContext.User
                  .Identity.Name);            
            }
        }
        public async Task<string> ResetPasswordAsync(ResetPasswordVM model)
        {
            if (model.NewPassword != model.ConfirmPassword)
                return "Password doesn't match its confirmation";
            var result = await _userManager.ChangePasswordAsync(_user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
                return "Good";
            return "Something went wrong";
        }
    }
}
