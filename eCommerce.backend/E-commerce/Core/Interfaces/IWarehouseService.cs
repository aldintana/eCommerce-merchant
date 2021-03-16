using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IWarehouseService
    {
        void Create();
        List<WarehouseGetVM> Get(string Date=null);
    }
}
