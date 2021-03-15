using Data.EntityModels;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface ICouponService
    {
        public void Create(CouponAddVM coupon);
        public List<CouponGetVM> GetAll();
    }
}
