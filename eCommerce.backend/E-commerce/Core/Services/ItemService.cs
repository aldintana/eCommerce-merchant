using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class ItemService : IItemService
    {
        private E_commerceDB _context;
        private IItemCostHistoryService _itemCostHistoryService;
        public ItemService(E_commerceDB context, IItemCostHistoryService itemCostHistoryService)
        {
            _context = context;
            _itemCostHistoryService = itemCostHistoryService;
        }
        public Item AddItem(Item item)
        {
            item.SerialNumber = new Random().Next(10000, 99999).ToString();
            _context.Item.Add(item);
            _context.SaveChanges();
            var newItem = _context.Item.Include(x => x.GenderSubCategory).ThenInclude(x => x.SubCategory)
                .First(x=>x.ID==item.ID);
            var list = _context.Size.Where(x => x.CategoryID == newItem.GenderSubCategory.SubCategory.CategoryID).ToList();
            foreach (var x in list)
            {
                _context.ItemSize.Add(new ItemSize
                {
                    ItemID = item.ID,
                    SizeID = x.ID
                });
                _context.SaveChanges();
            }
            return item;
        }

        public void DeleteItem(int id)
        {
            var item = _context.Item.First(x => x.ID == id);
            var list = _context.ItemImage.Where(x => x.ItemID == item.ID).ToList();
            foreach (var x in list)
            {
                _context.ItemImage.Remove(x);
                _context.SaveChanges();
            }
            _context.Item.Remove(item);
            _context.SaveChanges();
        }

        public void EditItem(Item item)
        {
            var editedItem = _context.Item.First(x => x.ID == item.ID);
            editedItem.Name = item.Name;
            if (editedItem.Price != item.Price)
            {              
                ItemCostHistory itemCostHistory = new ItemCostHistory
                {
                    PreviousPrice = editedItem.Price,
                    CurrentPrice = item.Price,
                    ItemID = editedItem.ID,
                    ModifiedDate = DateTime.Now
                };
                _itemCostHistoryService.Add(itemCostHistory);
                editedItem.Price = item.Price;
            }
            editedItem.Description = item.Description;
            editedItem.BrandCategoryID = item.BrandCategoryID;
            editedItem.GenderSubCategoryID = item.GenderSubCategoryID;
            _context.Entry(editedItem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Item> GetAll()
        {
            return _context.Item.ToList();
        }

        public Item GetItem(int id)
        {
            return _context.Item.First(x => x.ID == id);
        }
    }
}
