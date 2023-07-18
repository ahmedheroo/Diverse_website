using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            //switch (statusCode)
            //{
            //    case 404:
            //        ViewBag.errormessage = "sorry";
              
            //        break;
            //    case 500:
            //        ViewBag.errormessage = "sorry";

            //        break;
            //}
            return View("PageNotFound");
        }
    }
}
