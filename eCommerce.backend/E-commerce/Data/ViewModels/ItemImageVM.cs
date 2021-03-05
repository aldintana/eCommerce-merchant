using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.ViewModels
{
    public class ItemImageVM
    {
        public int ItemID { get; set; }
        public IFormFile Image { get; set; }
    }
}
