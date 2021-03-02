using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class SizeService : ISizeService
    {
        private E_commerceDB _context;
        public SizeService(E_commerceDB context)
        {
            _context = context;
        }
        public Size AddSize(Size size)
        {
            _context.Size.Add(size);
            _context.SaveChanges();
            return size;
        }

        public void DeleteSize(int id)
        {
            Size size = _context.Size.First(x => x.ID == id);
            _context.Size.Remove(size);
            _context.SaveChanges();
        }

        public void EditSize(Size size)
        {
            var editedSize = _context.Size.First(x => x.ID == size.ID);
            editedSize.Name = size.Name;
            editedSize.CategoryID = size.CategoryID;
            _context.Entry(editedSize).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Size> GetAll()
        {
            return _context.Size.ToList();
        }

        public Size GetSize(int id)
        {
            return _context.Size.First(x => x.ID == id);
        }
    }
}
