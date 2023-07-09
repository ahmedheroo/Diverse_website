using System;
using System.Collections.Generic;

#nullable disable

namespace Diverse_website.Models
{
    public partial class Vendor
    {
        public int Id { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string DescAr { get; set; }
        public string DescEn { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string PhotoUrl { get; set; }
        public string VendorUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
