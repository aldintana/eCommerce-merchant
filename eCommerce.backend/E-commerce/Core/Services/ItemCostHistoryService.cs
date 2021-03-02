using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class ItemCostHistoryService : IItemCostHistoryService
    {
        private E_commerceDB _context;
        public ItemCostHistoryService(E_commerceDB context)
        {
            _context = context;
        }

        public ItemCostHistory Add(ItemCostHistory itemCostHistory)
        {
            _context.ItemCostHistory.Add(itemCostHistory);
            _context.SaveChanges();
            return itemCostHistory;
        }

        public List<ItemCostHistory> GetAll()
        {
            return _context.ItemCostHistory.ToList();
        }

        public List<ItemCostHistory> GetByItemId(int itemid)
        {
            return _context.ItemCostHistory.Where(x => x.ItemID == itemid).ToList();
        }
    }
}
