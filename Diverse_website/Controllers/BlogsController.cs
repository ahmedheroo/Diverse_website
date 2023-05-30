using Microsoft.AspNetCore.Mvc;

namespace Diverse_website.Controllers
{
    public class BlogsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
