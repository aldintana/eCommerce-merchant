using Data.EntityModels;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IItemImageService
    {
        ItemImage Add(IFormFile file, int itemId);
        ItemImage GetById(int itemId);
        List<ItemImage> GetAll(int itemId);
        List<ItemImage> GetAll();
        void Delete(byte[] image, int itemId);
    }
}
