using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class BrandCategoryService : IBrandCategoryService
    {
        private E_commerceDB _context;
        public BrandCategoryService(E_commerceDB context)
        {
            _context = context;
        }
        public BrandCategory AddBrandCategory(BrandCategory brandCategory)
        {
            _context.BrandCategory.Add(brandCategory);
            _context.SaveChanges();
            return brandCategory;
        }

        public void DeleteBrandCategory(int id)
        {
            var brandCategory = _context.BrandCategory.First(x => x.BrandCategoryID == id);
            _context.BrandCategory.Remove(brandCategory);
            _context.SaveChanges();
        }

        public void EditBrandCategory(BrandCategory brandCategory)
        {
            var editedBrandCategory = _context.BrandCategory.First(x => x.BrandCategoryID == brandCategory.BrandCategoryID);
            editedBrandCategory.Name = brandCategory.Name;
            _context.Entry(editedBrandCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public List<BrandCategory> GetAll()
        {
            return _context.BrandCategory.ToList();
        }

        public BrandCategory GetBrandCategory(int id)
        {
            return _context.BrandCategory.First(x => x.BrandCategoryID == id);
        }
    }
}
