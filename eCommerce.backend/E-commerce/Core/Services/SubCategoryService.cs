using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private E_commerceDB _context;
        public SubCategoryService(E_commerceDB context)
        {
            _context = context;
        }
        public SubCategory AddSubCategory(SubCategory subCategory)
        {
            _context.SubCategory.Add(subCategory);
            _context.SaveChanges();
            return subCategory;
        }

        public void DeleteSubCategory(int id)
        {
            var subCategory = _context.SubCategory.First(x => x.SubCategoryID == id);
            _context.SubCategory.Remove(subCategory);
            _context.SaveChanges();
        }

        public void EditSubCategory(SubCategory subCategory)
        {
            var editedSubCategory = _context.SubCategory.First(x => x.SubCategoryID == subCategory.SubCategoryID);
            editedSubCategory.Name = subCategory.Name;
            editedSubCategory.CategoryID = subCategory.CategoryID;       
            _context.Entry(editedSubCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public List<SubCategory> GetAll()
        {
            return _context.SubCategory.ToList();
        }

        public SubCategory GetSubCategory(int id)
        {
            return _context.SubCategory.First(x => x.SubCategoryID == id);
        }
    }
}
