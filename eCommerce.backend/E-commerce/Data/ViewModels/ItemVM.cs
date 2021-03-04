using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class ItemVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public int BrandCategoryID { get; set; }
        public int GenderSubCategoryID { get; set; }
        public IFormFile Image { get; set; }
    }
}
