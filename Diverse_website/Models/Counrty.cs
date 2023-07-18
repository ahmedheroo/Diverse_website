using System;
using System.Collections.Generic;

#nullable disable

namespace Diverse_website.Models
{
    public partial class Counrty
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
