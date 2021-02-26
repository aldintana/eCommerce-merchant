using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IGenderCategoryService
    {
        GenderCategory AddGenderCategory(GenderCategory genderCategory);
        GenderCategory GetGenderCategory(int id);
        List<GenderCategory> GetAll();
        void DeleteGenderCategory(int id);
        void EditGenderCategory(GenderCategory genderCategory);
    }
}
