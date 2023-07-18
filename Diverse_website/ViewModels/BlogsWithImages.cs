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
        public Blog Blog { get; set; }
        public AspNetUser user { get; set; }
        public Counrty counrty { get; set; }

        public IEnumerable<Blog> LatestBlogs { get; set; }
        public IFormFile BlogImage { get; set; }
        public string SelectedCountryId { get; set; }
        public IEnumerable<Counrty> CountryList { get; set; }

    }
}
