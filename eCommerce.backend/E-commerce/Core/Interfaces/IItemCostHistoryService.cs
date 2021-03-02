using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IItemCostHistoryService
    {
        public ItemCostHistory Add(ItemCostHistory itemCostHistory);
        public List<ItemCostHistory> GetByItemId(int itemid);
        public List<ItemCostHistory> GetAll();
    }
}
