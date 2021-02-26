using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface ISubCategoryService
    {
        SubCategory AddSubCategory(SubCategory subCategory);
        SubCategory GetSubCategory(int id);
        List<SubCategory> GetAll();
        void DeleteSubCategory(int id);
        void EditSubCategory(SubCategory subCategory);
    }
}
