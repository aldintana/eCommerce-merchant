using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class ItemImageService : IItemImageService
    {
        private E_commerceDB _context;
        public ItemImageService(E_commerceDB context)
        {
            _context = context;
        }
        public ItemImage Add(IFormFile file, int itemId)
        {
            using(var ms=new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                ItemImage itemImage = new ItemImage
                {
                    Image = fileBytes,
                    ItemID = itemId
                };
                _context.ItemImage.Add(itemImage);
                _context.SaveChanges();
                return itemImage;
            }
        }

        public List<ItemImage> GetAll(int itemId)
        {
            return _context.ItemImage.Where(x => x.ItemID == itemId).ToList();
        }

        public ItemImage GetById(int itemId)
        {
            return _context.ItemImage.First(x => x.ItemID == itemId);
        }
    }
}
