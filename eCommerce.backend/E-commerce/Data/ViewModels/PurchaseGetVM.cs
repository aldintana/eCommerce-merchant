using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class PurchaseGetVM
    {
        public int ID { get; set; }
        public string ItemSizeName { get; set; }
        public int ItemSizeId { get; set; }
        public string BranchName { get; set; }
        public int BranchId { get; set; }
        public string DateTime { get; set; }
        public int Quantity { get; set; }
    }
}
