using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Diverse_website.Areas.Identity.Pages.Account
{
    
    public class AccessDeniedModel : PageModel
    {
        public void OnGet()
        {
            RedirectToAction("Index", "Blogs");
        }
    }
}

