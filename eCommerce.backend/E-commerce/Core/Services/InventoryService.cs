﻿using Core.Interfaces;
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
    public class InventoryService : IInventoryService
    {
        private E_commerceDB _context;
        private Account _user;
        private UserManager<Account> _userManager;
        public InventoryService(E_commerceDB context
            ,IHttpContextAccessor httpContextAccessor, UserManager<Account> userManager)
        {
            _context = context;
            _user = _context.Account.First(x => x.UserName == httpContextAccessor
              .HttpContext.User.Identity.Name);
            _userManager = userManager;
        }
        public List<InventoryVM> Get(string search=null)
        {
            var roles = _userManager.GetRolesAsync(_user);
            string role = "";
            if (roles.Result.Count != 0)
                role = roles.Result[0];
            if (role == "Director" || role == "Warehouseman")
            {
                Employee employee = _context.Employee.First(x => x.ID == _user.Id);
                if (String.IsNullOrWhiteSpace(search))
                {
                    var list = _context.Inventory.Include(x => x.ItemSize).ThenInclude(x => x.Item)
                    .Include(x => x.ItemSize).ThenInclude(x => x.Size)
                    .Include(x => x.Branch)
                    .Where(x => x.BranchID == employee.BranchID)
                    .Select(
                        x => new InventoryVM
                        {
                            ID = x.ID,
                            IsAvailable = x.IsAvailable,
                            BranchID = x.BranchID,
                            ItemSizeID = x.ItemSizeID,
                            Quantity = x.Quantity,
                            ItemSizeName = x.ItemSize.Item.Name + " - " + x.ItemSize.Size.Name,
                            BranchName = x.Branch.Name
                        }
                    ).ToList();
                    return list;
                }
                else
                {
                    var list = _context.Inventory.Include(x => x.ItemSize).ThenInclude(x => x.Item)
                    .Include(x => x.ItemSize).ThenInclude(x => x.Size)
                    .Include(x => x.Branch)
                    .Where(x => x.BranchID == employee.BranchID)
                    .Select(
                        x => new InventoryVM
                        {
                            ID = x.ID,
                            IsAvailable = x.IsAvailable,
                            BranchID = x.BranchID,
                            ItemSizeID = x.ItemSizeID,
                            Quantity = x.Quantity,
                            ItemSizeName = x.ItemSize.Item.Name + " - " + x.ItemSize.Size.Name,
                            BranchName = x.Branch.Name
                        }
                    ).ToList();
                    return list.Where(x => x.ItemSizeName.ToLower().Contains(search.ToLower())).ToList();
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(search))
                {
                    var list = _context.Inventory.Include(x => x.ItemSize).ThenInclude(x => x.Item)
                    .Include(x => x.ItemSize).ThenInclude(x => x.Size)
                    .Include(x => x.Branch)
                    .Select(
                        x => new InventoryVM
                        {
                            ID = x.ID,
                            IsAvailable = x.IsAvailable,
                            BranchID = x.BranchID,
                            ItemSizeID = x.ItemSizeID,
                            Quantity = x.Quantity,
                            ItemSizeName = x.ItemSize.Item.Name + " - " + x.ItemSize.Size.Name,
                            BranchName = x.Branch.Name
                        }
                    ).ToList();
                    return list;
                }
                else
                {
                    var list = _context.Inventory.Include(x => x.ItemSize).ThenInclude(x => x.Item)
                    .Include(x => x.ItemSize).ThenInclude(x => x.Size)
                    .Include(x => x.Branch)
                    .Select(
                        x => new InventoryVM
                        {
                            ID = x.ID,
                            IsAvailable = x.IsAvailable,
                            BranchID = x.BranchID,
                            ItemSizeID = x.ItemSizeID,
                            Quantity = x.Quantity,
                            ItemSizeName = x.ItemSize.Item.Name + " - " + x.ItemSize.Size.Name,
                            BranchName = x.Branch.Name
                        }
                    ).ToList();
                    return list.Where(x => x.ItemSizeName.ToLower().Contains(search.ToLower())).ToList();
                }
            }
        }

        public Inventory GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
