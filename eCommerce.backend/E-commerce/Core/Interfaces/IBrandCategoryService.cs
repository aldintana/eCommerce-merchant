using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IBrandCategoryService
    {
        BrandCategory AddBrandCategory(BrandCategory brandCategory);
        BrandCategory GetBrandCategory(int id);
        List<BrandCategory> GetAll();
        void DeleteBrandCategory(int id);
        void EditBrandCategory(BrandCategory brandCategory);
    }
}
