using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class WarehouseReportVM
    {
        public int Id { get; set; }
        public string ItemSizeName { get; set; }
        public string BranchName { get; set; }
        public int PreviousBalance { get; set; }
        public int Income { get; set; }
        public int Sold { get; set; }
        public int CurrentBalance { get; set; }
        public string Date { get; set; }
    }
}
