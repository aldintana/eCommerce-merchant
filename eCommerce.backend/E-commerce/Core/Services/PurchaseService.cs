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
    public class PurchaseService : IPurchaseService
    {
        private E_commerceDB _context;
        private Account _user;
        private UserManager<Account> _userManager;
        public PurchaseService(E_commerceDB context, IHttpContextAccessor httpContextAccessor,
            UserManager<Account> userManager)
        {
            _context = context;
            _user = _context.Account.First(x => x.UserName == httpContextAccessor.HttpContext
              .User.Identity.Name);
            _userManager = userManager;
        }
        public Purchase Add(PurchaseAddVM purchaseAddVM)
        {
            Employee employee = _context.Employee
                .Include(x=>x.Branch)
                .First(x => x.ID == _user.Id);

            Purchase purchase = new Purchase
            {
                BranchID = employee.BranchID,
                Date = DateTime.Now,
                ItemSizeID = purchaseAddVM.ItemSizeId,
                Quantity = purchaseAddVM.Quantity,
                EmployeeID = employee.ID
            };
            _context.Purchase.Add(purchase);
            _context.SaveChanges();

            Inventory inventory = _context.Inventory.First(x => x.BranchID == purchase.BranchID &&
              x.ItemSizeID == purchase.ItemSizeID);
            inventory.Quantity += purchaseAddVM.Quantity;
            if (inventory.Quantity > 0)
                inventory.IsAvailable = true;
            _context.Entry(inventory).State = EntityState.Modified;
            _context.SaveChanges();
            return purchase;
        }

        public List<PurchaseGetVM> Get()
        {
            
            Employee employee = _context.Employee
                .Include(x => x.Branch)
                .First(x => x.ID == _user.Id);

            var list = _context.Purchase.Include(x => x.ItemSize).ThenInclude(x => x.Item)
                .Include(x => x.ItemSize).ThenInclude(x => x.Size)
                .Where(x => x.BranchID == employee.BranchID&&x.EmployeeID==employee.ID)
                .Select
                (
                    x => new PurchaseGetVM
                    {
                        ID = x.ID,
                        ItemSizeId=x.ItemSizeID,
                        DateTime = x.Date.ToString("dd.MM.yyyy. HH:mm"),
                        ItemSizeName = x.ItemSize.Item.Name + " - " + x.ItemSize.Size.Name,
                        Quantity = x.Quantity
                    }
                ).ToList();
            return list;
        }
    }
}
