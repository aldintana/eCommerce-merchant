using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class WarehouseService : IWarehouseService
    {
        private E_commerceDB _context;
        private Account _user;
        private UserManager<Account> _userManager;
        public WarehouseService(E_commerceDB context
            , IHttpContextAccessor httpContextAccessor, UserManager<Account> userManager)
        {
            _context = context;
            _user = _context.Account.First(x => x.UserName == httpContextAccessor
              .HttpContext.User.Identity.Name);
            _userManager = userManager;
        }
        public void Create()
        {                       
            Employee employee = _context.Employee.First(x => x.ID == _user.Id);
           
            var listItemSize = _context.ItemSize.ToList();
            foreach (var item in listItemSize)
            {
                int currentBalance = _context.Inventory.Where(x => x.ItemSizeID == item.ID && x.BranchID == employee.BranchID)
                    .Select(x => x.Quantity).FirstOrDefault();
                int income = _context.Purchase.Where(x => x.ItemSizeID == item.ID && 
                x.BranchID == employee.BranchID && x.Date.Date==DateTime.Now.Date)
                    .Select(x => x.Quantity).Sum();
                int sold = 0;
                var previous = _context.Warehouse.Where(x => x.ItemSizeID == item.ID && x.BranchID == employee.BranchID)
                    .OrderBy(x => x.Date).ThenByDescending(x => x.Date).FirstOrDefault();
                int previousBalance = 0;
                if(previous!=null)
                {
                    previousBalance = _context.Warehouse.Where(x => x.ItemSizeID == item.ID && x.BranchID == employee.BranchID)
                    .OrderBy(x => x.Date).ThenByDescending(x => x.Date).Select(x => x.CurrentBalance).FirstOrDefault();
                }
                Warehouse warehouse = new Warehouse
                {
                    BranchID = employee.BranchID,
                    CurrentBalance = currentBalance,
                    Income = income,
                    Sold = sold,
                    ItemSizeID = item.ID,
                    Date = DateTime.Now,
                    PreviousBalance = previousBalance
                };
                _context.Warehouse.Add(warehouse);
                _context.SaveChanges();
                
            }
        }

        public List<WarehouseGetVM> Get(string Date=null)
        {
            var roles = _userManager.GetRolesAsync(_user);
            string role = "";
            if (roles.Result.Count != 0)
                role = roles.Result[0];

            if(role=="Director")
            {
                if (!string.IsNullOrWhiteSpace(Date))
                {
                    DateTime filter = DateTime.ParseExact(Date, "dd.MM.yyyy.", null);
                    Employee employee = _context.Employee.First(x => x.ID == _user.Id);
                    var list = _context.Warehouse
                    .Include(x => x.Branch).Include(x => x.ItemSize).ThenInclude(x => x.Item)
                    .Include(x => x.ItemSize).ThenInclude(x => x.Size)
                    .Where(x => x.BranchID == employee.BranchID && x.Date.Date == filter.Date)
                    .Select(
                    x => new WarehouseGetVM
                    {
                        Branch = x.Branch.Name,
                        CurrentBalance = x.CurrentBalance,
                        Date = x.Date.ToString("dd.MM.yyyy."),
                        Income = x.Income,
                        ID = x.ID,
                        ItemSize = x.ItemSize.Item.Name + " - " + x.ItemSize.Size.Name,
                        PreviousBalance = x.PreviousBalance,
                        Sold = x.Sold
                    }
                    ).ToList();
                    return list;
                }
                else
                {
                    Employee employee = _context.Employee.First(x => x.ID == _user.Id);
                    var list = _context.Warehouse
                    .Include(x => x.Branch).Include(x => x.ItemSize).ThenInclude(x => x.Item)
                    .Include(x => x.ItemSize).ThenInclude(x => x.Size)
                    .Where(x => x.BranchID == employee.BranchID)
                    .Select(
                    x => new WarehouseGetVM
                    {
                        Branch = x.Branch.Name,
                        CurrentBalance = x.CurrentBalance,
                        Date = x.Date.ToString("dd.MM.yyyy."),
                        Income = x.Income,
                        ID = x.ID,
                        ItemSize = x.ItemSize.Item.Name + " - " + x.ItemSize.Size.Name,
                        PreviousBalance = x.PreviousBalance,
                        Sold = x.Sold
                    }
                    ).ToList();
                    return list;
                }
            }
            else if(role=="Admin")
            {
                if (!string.IsNullOrWhiteSpace(Date))
                {
                    DateTime filter = Convert.ToDateTime(Date);
                    Employee employee = _context.Employee.First(x => x.ID == _user.Id);
                    var list = _context.Warehouse
                    .Include(x => x.Branch).Include(x => x.ItemSize).ThenInclude(x => x.Item)
                    .Include(x => x.ItemSize).ThenInclude(x => x.Size)
                    .Where(x=>x.Date.Date==filter.Date)
                    .Select(
                    x => new WarehouseGetVM
                    {
                        Branch = x.Branch.Name,
                        CurrentBalance = x.CurrentBalance,
                        Date = x.Date.ToString("dd.MM.yyyy."),
                        Income = x.Income,
                        ID = x.ID,
                        ItemSize = x.ItemSize.Item.Name + " - " + x.ItemSize.Size.Name,
                        PreviousBalance = x.PreviousBalance,
                        Sold = x.Sold
                    }
                    ).ToList();
                    return list;
                }
                else
                {
                    Employee employee = _context.Employee.First(x => x.ID == _user.Id);
                    var list = _context.Warehouse
                    .Include(x => x.Branch).Include(x => x.ItemSize).ThenInclude(x => x.Item)
                    .Include(x => x.ItemSize).ThenInclude(x => x.Size)
                    .Select(
                    x => new WarehouseGetVM
                    {
                        Branch = x.Branch.Name,
                        CurrentBalance = x.CurrentBalance,
                        Date = x.Date.ToString("dd.MM.yyyy."),
                        Income = x.Income,
                        ID = x.ID,
                        ItemSize = x.ItemSize.Item.Name + " - " + x.ItemSize.Size.Name,
                        PreviousBalance = x.PreviousBalance,
                        Sold = x.Sold
                    }
                    ).ToList();
                    return list;
                }
                
            }
            else
            {
                return new List<WarehouseGetVM>();
            }
        }
    }
}
