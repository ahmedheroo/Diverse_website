using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Diverse_website.Models
{
    public partial class Vendor
    {
        
        public int Id { get; set; }
        [Required]

        public string TitleAr { get; set; }
        [Required]

        public string TitleEn { get; set; }
        [Required]
        public string TitleFr { get; set; }
        [Required]

        public string DescAr { get; set; }
        [Required]

        public string DescEn { get; set; }
        [Required]

        public string DescFr { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string PhotoUrl { get; set; }
        [Required]

        public string VendorUrl { get; set; }
        public bool IsDeleted { get; set; }
        [Required]

        public int? CountryId { get; set; }
        public virtual Counrty Counrty { get; set; }

    }
}
