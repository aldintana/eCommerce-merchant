using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class CategoryService : ICategoryService
    {
        private E_commerceDB _context;
        public CategoryService(E_commerceDB context)
        {
            _context = context;
        }
        public Category AddCategory(Category category)
        {
            _context.Category.Add(category);
            _context.SaveChanges();
            return category;
        }

        public void DeleteCategory(int id)
        {
            var category = _context.Category.First(x => x.CategoryID == id);
            _context.Category.Remove(category);
            _context.SaveChanges();
        }

        public void EditCategory(Category category)
        {
            var editedCategory = _context.Category.First(x => x.CategoryID == category.CategoryID);
            editedCategory.Name = category.Name;
            _context.Entry(editedCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public List<Category> GetAll()
        {
            return _context.Category.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Category.First(x => x.CategoryID == id);
        }
    }
}
