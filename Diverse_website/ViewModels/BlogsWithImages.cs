using Diverse_website.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.ViewModels
{
    public class BlogsWithImages
    {
        public blog Blog { get; set; }
        public IFormFile BlogImage { get; set; }
    }
}
