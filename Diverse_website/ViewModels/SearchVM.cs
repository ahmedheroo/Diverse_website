using Diverse_website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.ViewModels
{
    public class SearchVM
    {
        public IEnumerable<Blog> LatestBlogs { get; set; }
        public string result { get; set; }
    }
}
