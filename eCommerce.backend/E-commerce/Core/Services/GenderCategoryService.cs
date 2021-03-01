using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class GenderCategoryService : IGenderCategoryService
    {
        private E_commerceDB _context;
        public GenderCategoryService(E_commerceDB context)
        {
            _context = context;
        }
        public GenderCategory AddGenderCategory(GenderCategory genderCategory)
        {
            _context.GenderCategory.Add(genderCategory);
            _context.SaveChanges();
            return genderCategory;
        }

        public void DeleteGenderCategory(int id)
        {
            var genderCategory = _context.GenderCategory.First(x => x.ID == id);
            _context.GenderCategory.Remove(genderCategory);
            _context.SaveChanges();
        }

        public void EditGenderCategory(GenderCategory genderCategory)
        {
            var editedGenderCategory = _context.GenderCategory.First(x => x.ID == genderCategory.ID);
            editedGenderCategory.Name = genderCategory.Name;
            _context.Entry(editedGenderCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public List<GenderCategory> GetAll()
        {
            return _context.GenderCategory.ToList();
        }

        public GenderCategory GetGenderCategory(int id)
        {
            return _context.GenderCategory.First(x => x.ID == id);
        }
    }
}
