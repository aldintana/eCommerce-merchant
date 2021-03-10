using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IInventoryService
    {
        List<Inventory> Get();
        Inventory GetById(int id);
    }
}
