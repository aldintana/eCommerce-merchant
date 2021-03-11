using Data.EntityModels;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IInventoryService
    {
        List<InventoryVM> Get(string search=null);
        Inventory GetById(int id);
    }
}
