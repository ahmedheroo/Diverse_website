using Diverse_website.Models;
using Diverse_website.Repository;
using Diverse_website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserRepo userRepo;
        public UserController(UserManager<IdentityUser> _userManager,RoleManager<IdentityRole> _roleManager, IUserRepo _userRepo) 
        {
            userRepo = _userRepo;
            userManager = _userManager;
            roleManager = _roleManager;
        }
        public IActionResult Index()
        {
            IEnumerable<AspNetUserRole> model= userRepo.GetAllUsers();
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {

            User_RoleVM model = new User_RoleVM()
            {
                RoleList = userRepo.GetAllRoles().Where(x => x.Name != "SuperAdmin").ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(User_RoleVM model)
        {
            var user = new IdentityUser
            {
                UserName = model.User.UserName,
                Email = model.User.Email,
                PhoneNumber = model.User.PhoneNumber
            };
            string roleName = "";
            IdentityResult result = new IdentityResult();
            if (model.User.PasswordHash != null)
            {
                result = await userManager.CreateAsync(user, model.User.PasswordHash);
            }
            if (result.Succeeded)
            {
                roleName = userRepo.GetRoleNameUsingRoleId(model.SelectedRoleId);
                await userManager.AddToRoleAsync(user, roleName);
            }
            else
            {
                ////ErrorMessage
            }
            if (roleName == "User")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");

            }
        }


    }
}
