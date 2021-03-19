using ClosedXML.Excel;
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
            if (httpContextAccessor.HttpContext.User.Identity.Name != null)
            {
                _user = _context.Account.First(x => x.UserName == httpContextAccessor
                  .HttpContext.User.Identity.Name);
            }
            _userManager = userManager;
        }
        public void Create()
        {
            Employee employee = _context.Employee.First(x => x.ID == _user.Id);
            DateTime lastDate = _context.WarehouseLog.Where(x => x.BranchID == employee.BranchID)
                .OrderByDescending(x => x.Date)
                .Select(x => x.Date)
                .FirstOrDefault();
            
            bool exist=false;
            if (lastDate != null)
                exist = true;
            if(lastDate.Date==DateTime.Today.Date)
            {
                throw new Exception("Limit");
            }
            var listItemSize = _context.ItemSize.ToList();
            foreach (var item in listItemSize)
            {
                int currentBalance = _context.Inventory.Where(x => x.ItemSizeID == item.ID && x.BranchID == employee.BranchID)
                    .Select(x => x.Quantity).FirstOrDefault();
                int income = 0;
                int sold = 0;
                int sold2 = 0;
                if(!exist)
                {
                    income = _context.Purchase.Where(x => x.ItemSizeID == item.ID &&
                    x.BranchID == employee.BranchID)
                    .Select(x => x.Quantity).Sum();
                    sold = 0;
                    sold2 = _context.Order
                        .Include(x => x.ShoppingCart)
                        .Where(x => x.ItemSizeID == item.ID && x.ShoppingCart.BranchID == employee.BranchID)
                        .Select(x => x.Quantity).Sum();
                }
                else
                {
                    income = _context.Purchase.Where(x => x.ItemSizeID == item.ID &&
                    x.BranchID == employee.BranchID && x.Date > lastDate)
                    .Select(x => x.Quantity).Sum();
                    sold = 0;
                    sold2 = _context.Order
                        .Include(x => x.ShoppingCart)
                        .Where(x => x.ItemSizeID == item.ID && x.ShoppingCart.BranchID == employee.BranchID
                        && x.Date>lastDate)
                        .Select(x => x.Quantity).Sum();
                }

                var previous = _context.Warehouse.Where(x => x.ItemSizeID == item.ID && x.BranchID == employee.BranchID)
                    .OrderBy(x => x.Date).FirstOrDefault();
                int previousBalance = 0;
                if (previous != null)
                {
                    previousBalance = _context.Warehouse
                    .Where(x => x.ItemSizeID == item.ID && x.BranchID == employee.BranchID)
                    .OrderByDescending(x => x.Date).Select(x => x.CurrentBalance).FirstOrDefault();
                }
                Warehouse warehouse = new Warehouse
                {
                    BranchID = employee.BranchID,
                    CurrentBalance = currentBalance,
                    Income = income,
                    Sold = sold2,
                    ItemSizeID = item.ID,
                    Date = DateTime.Now,
                    PreviousBalance = previousBalance
                };
                _context.Warehouse.Add(warehouse);
                _context.SaveChanges();
            }
            WarehouseLog warehouseLog = new WarehouseLog
            {
                BranchID = employee.BranchID,
                Date = DateTime.Now
            };
            _context.WarehouseLog.Add(warehouseLog);
            _context.SaveChanges();
        }

        public List<WarehouseGetVM> Get(string Date = null)
        {
            var roles = _userManager.GetRolesAsync(_user);
            string role = "";
            if (roles.Result.Count != 0)
                role = roles.Result[0];

            if (role == "Director")
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
            else if (role == "Admin")
            {
                if (!string.IsNullOrWhiteSpace(Date))
                {
                    DateTime filter = Convert.ToDateTime(Date);
                    var list = _context.Warehouse
                    .Include(x => x.Branch).Include(x => x.ItemSize).ThenInclude(x => x.Item)
                    .Include(x => x.ItemSize).ThenInclude(x => x.Size)
                    .Where(x => x.Date.Date == filter.Date)
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

        public IEnumerable<string> GetDates()
        {
            Employee employee = _context.Employee.First(x => x.ID == _user.Id);
            var list = _context.Warehouse
                .Where(x => x.BranchID == employee.BranchID)
                .Select(x => x.Date.ToString("dd.MM.yyyy.")).ToList();
            return list.Distinct();
        }

        public List<WarehouseReportVM> GetReport(string date)
        {
            DateTime filter = DateTime.ParseExact(date, "dd.MM.yyyy.", null);
            var list = _context.Warehouse
                .Include(x => x.Branch)
                .Include(x => x.ItemSize)
                .ThenInclude(x => x.Item)
                .Include(x => x.ItemSize)
                .ThenInclude(x => x.Size)
                .Where(x => x.BranchID == 1 && x.Date.Date == filter.Date)
                .Select
                (
                    x => new WarehouseReportVM
                    {
                        BranchName = x.Branch.Name,
                        CurrentBalance = x.CurrentBalance,
                        Date = x.Date.ToString("dd.MM.yyyy."),
                        Id = x.ID,
                        Income = x.Income,
                        ItemSizeName = x.ItemSize.Item.Name + " - " + x.ItemSize.Size.Name,
                        PreviousBalance = x.PreviousBalance,
                        Sold = x.Sold
                    }
                ).ToList();
            return list;
        }
    }
}