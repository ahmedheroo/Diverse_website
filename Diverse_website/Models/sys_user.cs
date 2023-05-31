using System;
using System.ComponentModel.DataAnnotations;

namespace Diverse_website.Models
{
    public class sys_user
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public DateTime LastLogin { get; set; }
        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
    }
}
