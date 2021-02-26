using Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface ISizeService
    {
        Size AddSize(Size size);
        Size GetSize(int id);
        List<Size> GetAll();
        void DeleteSize(int id);
        void EditSize(Size size);
    }
}
