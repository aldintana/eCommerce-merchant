using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class WarehouseGetVM
    {
        public int ID { get; set; }
        public int PreviousBalance { get; set; }
        public int Income { get; set; }
        public int Sold { get; set; }
        public int CurrentBalance { get; set; }
        public string Date { get; set; }
        public string ItemSize { get; set; }
        public string Branch { get; set; }
    }
}
