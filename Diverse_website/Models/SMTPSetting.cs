using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Models
{
    public class SMTPSetting
    {
        public int Id { get; set; }
        [Required]
        public string HostName { get; set; }
        [Required]
        public string EmailFrom { get; set; }
        [Required]
        public string PassWordEmailFrom { get; set; }
        [Required]
        public string EmailTo { get; set; }
    }
}
