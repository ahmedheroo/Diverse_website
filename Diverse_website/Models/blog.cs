using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Models
{
    public class blog
    {
        [Key]
        public int ID { get; set; }
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
