using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class InventoryVM
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
        public string BranchName { get; set; }
        public int BranchID { get; set; }       
        public int ItemSizeID { get; set; }
        public string ItemSizeName { get; set; }
    }
}
