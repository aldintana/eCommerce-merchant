using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface ICategoryService
    {
        Category AddCategory(Category category);
        Category GetCategory(int id);
        List<Category> GetAll();
        void DeleteCategory(int id);
        void EditCategory(Category category);
    }
}
