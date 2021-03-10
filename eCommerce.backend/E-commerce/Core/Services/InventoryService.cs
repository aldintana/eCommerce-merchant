using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class InventoryService : IInventoryService
    {
        private E_commerceDB _context;
        private Account _user;
        public InventoryService(E_commerceDB context
            ,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Account.First(x => x.UserName == httpContextAccessor
              .HttpContext.User.Identity.Name);
        }
        public List<Inventory> Get()
        {
            Employee employee = _context.Employee.First(x => x.ID == _user.Id);

            var list = _context.Inventory.Where(x => x.BranchID == employee.BranchID).ToList();
            return list;
        }

        public Inventory GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
