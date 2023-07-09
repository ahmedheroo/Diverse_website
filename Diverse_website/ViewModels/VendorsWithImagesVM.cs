using Diverse_website.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.ViewModels
{
    public class VendorsWithImagesVM
    {
        public Vendor vendor { get; set; }
        public IFormFile VendorImage { get; set; }
    }
}
