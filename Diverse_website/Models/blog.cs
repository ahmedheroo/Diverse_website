using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Diverse_website.Models
{
    public partial class Blog
    {
        public int Id { get; set; }
        [Required]
        public string TitleAr { get; set; }
        [Required]
        public string TitleEn { get; set; }
        [Required]
        public string TitleFr { get; set; }
        [Required]

        public string ContentAr { get; set; }
        [Required]

        public string ContentEn { get; set; }
        [Required]

        public string ContentFr { get; set; }
        [Required]

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        

        public string PhotoUrl { get; set; }
        public bool IsDeleted { get; set; }
        [Required]

        public string Tag { get; set; }
        public string userid { get; set; }
        [Required]

        public int CountryId { get; set; }
        public virtual Counrty Counrty { get; set; }

    }
}
