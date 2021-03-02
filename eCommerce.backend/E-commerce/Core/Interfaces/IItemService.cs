using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IItemService
    {
        Item AddItem(Item item);
        Item GetItem(int id);
        List<Item> GetAll();
        void DeleteItem(int id);
        void EditItem(Item item);
    }
}
