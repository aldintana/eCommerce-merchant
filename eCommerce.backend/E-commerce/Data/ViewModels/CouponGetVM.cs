﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class CouponGetVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ExpiredDate { get; set; }
        public string Value { get; set; }
    }
}
