using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IGenderSubCategoryService
    {
        GenderSubCategory AddGenderSubCategory(GenderSubCategory genderSubCategory);
        GenderSubCategory GetGenderSubCategory(int id);
        List<GenderSubCategory> GetAll();
        void DeleteGenderSubCategory(int id);
        void EditGenderSubCategory(GenderSubCategory genderSubCategory);
    }
}
