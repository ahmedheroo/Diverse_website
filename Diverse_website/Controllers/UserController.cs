using Diverse_website.Data;
using Diverse_website.Models;
using Diverse_website.Repository;
using Diverse_website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Diverse_website.Helpers;
using System.Threading.Tasks;

namespace Diverse_website.Controllers
{
    [Authorize("AdminRole")]
   // [ControllerName("Test")]
    public class UserController : BaseController
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
      //  private readonly IDataProtector protector;
        private readonly IUserRepo userRepo;
       // public string encurl;
        public UserController(UserManager<IdentityUser> _userManager,RoleManager<IdentityRole> _roleManager, IUserRepo _userRepo/*,IDataProtectionProvider _protector*/) 
        {
            userRepo = _userRepo;
            userManager = _userManager;
            roleManager = _roleManager;
            //protector = _protector.CreateProtector("") ;
           // encurl=  protector.Protect("Protection string encrypted");
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
                RoleList = userRepo.GetAllRoles()
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
            //IEnumerable<AspNetUserRole> listofusers = userRepo.GetAllUsers();
            //string usermail=  listofusers.Select(s => s.User.UserName == user.UserName).First().ToString();
            //string username =  listofusers.Select(s => s.User.Email == user.Email).First().ToString();
            //listofusers.Select(s => s.User.Email == user.Email);
            IdentityResult result = new IdentityResult();
            //if (!result.Succeeded)
            //{
            //        NotifyAlert("error", "UserName Or Email already exist", NotificationType.error);
            //        return View();
            //}
            //if (!result.Succeeded && (usermail!=null || username != null))
            //{
            //    NotifyAlert("error", "UserName Or Email already exist" + usermail + " " + username, NotificationType.error);
            //    return View();
            //}

            if (model.User.PasswordHash != null)
            {
                result = await userManager.CreateAsync(user, model.User.PasswordHash);
            }
            //if (model.User.UserName.Any())
            //{
            //    NotifyAlert("error", "UserName already exist! ");
            //}
            if (result.Succeeded)
            {
                try
                {
                    NotifyAlert("success", "User has been Created ");
                    roleName = userRepo.GetRoleNameUsingRoleId(model.SelectedRoleId);
                    await userManager.AddToRoleAsync(user, roleName);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    NotifyAlert("error", "An error has occured !! ", NotificationType.error);
                    model.RoleList = userRepo.GetAllRoles();


                    return View(model);
                }


            }
            else
            {
               
                   
            
                NotifyAlert("error", "An error has occured !! ", NotificationType.error);
                return View();

            }
           
        }
        [HttpGet]
        public IActionResult Edit(string UserId)
        {
            AspNetUser user = userRepo.GetUserById(UserId);
            User_RoleVM model = new User_RoleVM()
            {
                RoleList = userRepo.GetAllRoles().ToList(),
                User = user,
                SelectedRoleId = userRepo.GetUserRoleId(UserId)
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User_RoleVM model)
        {
            try
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
                    try
                    {
                        NotifyAlert("success", "User has been Updated ");
                        roleName = userRepo.GetRoleNameUsingRoleId(model.SelectedRoleId);
                        await userManager.AddToRoleAsync(user, roleName);
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        NotifyAlert("error", "An error has occured !! ", NotificationType.error);
                        model.RoleList = userRepo.GetAllRoles();
                        return View(model);
                    }
                }

                else
                {
                        NotifyAlert("error", "An error has occured !! ", NotificationType.error);
                    model.RoleList = userRepo.GetAllRoles();
                    return View(model);

                    }

            }
            catch (Exception)
            {

                NotifyAlert("error", "An error has occured !! ", NotificationType.error);
                model.RoleList = userRepo.GetAllRoles();
                return View(model);
            }

          
           

        }
       
        public async Task<IActionResult> DeleteUser(string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);

            try
            {
                NotifyAlert("success", "User has been deleted");
                var result = await userManager.DeleteAsync(user);
                return RedirectToAction("Index");


            }
            catch (Exception)
            {
                NotifyAlert("error", "An error has occured !!", NotificationType.error);
                return RedirectToAction("Index");


            }
        }


    }
}
