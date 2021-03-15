using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class CouponAddVM
    {
        public CouponAddVM()
        {
            Quantity = 1;
        }
        public int Days { get; set; }
        public double Value { get; set; }
        public int Quantity { get; set; }
    }
}
