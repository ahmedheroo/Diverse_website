using Diverse_website.Models;
using Diverse_website.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Diverse_website.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserRepo userRepo;
        public UserController(IUserRepo _userRepo) 
        {
            userRepo = _userRepo;
        }
        public IActionResult Index()
        {
           IQueryable<sys_user> model= userRepo.GetUsersIncludeRoles();
            return View(model);
        }
        
    }
}
