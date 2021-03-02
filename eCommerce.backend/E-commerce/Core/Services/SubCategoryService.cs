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
        private IGenderCategoryService _genderCategoryService;
        private IGenderSubCategoryService _genderSubCategoryService;
        public SubCategoryService(E_commerceDB context, 
            IGenderCategoryService genderCategoryService, IGenderSubCategoryService genderSubCategoryService)
        {
            _context = context;
            _genderCategoryService = genderCategoryService;
            _genderSubCategoryService = genderSubCategoryService;
        }
        public SubCategory AddSubCategory(SubCategory subCategory)
        {
            try
            {
                _context.SubCategory.Add(subCategory);
                _context.SaveChanges();
                var list = _genderCategoryService.GetAll();
                foreach (var x in list)
                {
                    GenderSubCategory genderSubCategory = new GenderSubCategory
                    {
                        GenderCategoryID = x.ID,
                        SubCategoryID = subCategory.ID
                    };
                    _genderSubCategoryService.AddGenderSubCategory(genderSubCategory);
                }
                return subCategory;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public void DeleteSubCategory(int id)
        {
            var subCategory = _context.SubCategory.First(x => x.ID == id);
            _context.SubCategory.Remove(subCategory);
            _context.SaveChanges();
        }

        public void EditSubCategory(SubCategory subCategory)
        {
            var editedSubCategory = _context.SubCategory.First(x => x.ID == subCategory.ID);
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
            return _context.SubCategory.First(x => x.ID == id);
        }
    }
}
