using Data.EntityModels;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IPurchaseService
    {
        public Purchase Add(PurchaseAddVM purchaseAddVM);
        public List<PurchaseGetVM> Get();
    }
}
