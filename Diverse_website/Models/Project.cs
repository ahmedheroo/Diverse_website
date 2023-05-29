using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Models
{
    public class Project
    {
        [Key]
        public int ID { get; set; }
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string DescAr { get; set; }
        public string DescEn { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
