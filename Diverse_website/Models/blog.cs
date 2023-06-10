using System;
using System.Collections.Generic;

#nullable disable

namespace Diverse_website.Models
{
    public partial class Blog
    {
        public int Id { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string ContentAr { get; set; }
        public string ContentEn { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
