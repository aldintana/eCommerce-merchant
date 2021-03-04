using Core.Interfaces;
using Data.DbContext;
using Data.EntityModels;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Services
{
    public class GenderSubCategoryService : IGenderSubCategoryService
    {
        public E_commerceDB _context;
        
        public GenderSubCategoryService(E_commerceDB context)
        {
            _context = context;
        }
        public GenderSubCategory AddGenderSubCategory(GenderSubCategory genderSubCategory)
        {
            _context.GenderSubCategory.Add(genderSubCategory);
            _context.SaveChanges();
            return genderSubCategory;
        }

        public void DeleteGenderSubCategory(int id)
        {
            var genderSubCategory = _context.GenderSubCategory.First(x => x.ID == id);
            _context.GenderSubCategory.Remove(genderSubCategory);
            _context.SaveChanges();
        }

        public void EditGenderSubCategory(GenderSubCategory genderSubCategory)
        {

            throw new NotImplementedException();
        }

        public List<GenderSubCategoryViewModel> GetAll()
        {
            return _context.GenderSubCategory.Include(x => x.GenderCategory)
                .Include(x => x.SubCategory)
                .Select
                (
                    x => new GenderSubCategoryViewModel
                    {
                        Id = x.ID,
                        Name = $"{x.GenderCategory.Name} - {x.SubCategory.Name}"
                    }
                ).ToList();
        }

        public GenderSubCategory GetGenderSubCategory(int id)
        {
            return _context.GenderSubCategory.First(x => x.ID == id);
        }
    }
}
