using Diverse_website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.ViewModels
{
    public class User_RoleVM
    {
        public AspNetUser User { get; set; }
        public string SelectedRoleId  { get; set; }
        public IEnumerable<AspNetRole> RoleList { get; set; }
   
      
    }
}
