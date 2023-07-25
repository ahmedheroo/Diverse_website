using Diverse_website.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diverse_website.Repository
{
    public class UserRepo:IUserRepo
    {
        private readonly Diverse_websiteContext context;

        public UserRepo(Diverse_websiteContext _context)
        {
            context = _context;
        }

        public IEnumerable<AspNetUserRole> GetAllAdmins()
        {
            //return context.AspNetUsers.Include(x => x.AspNetUserRoles.Where(x=>x.Role.Name=="Admin")).ThenInclude(x => x.Role).ToList();
            return context.AspNetUserRoles.Include(x => x.Role).Where(x => x.Role.Name == "Admin").Include(x => x.User).ToList();
        }
        public IEnumerable<AspNetUserRole> GetAllUsers()
        {
            //  return context.AspNetUsers.Include(x => x.AspNetUserRoles.Where(x => x.Role.Name == "User")).ThenInclude(x=>x.Role).ToList();
            return context.AspNetUserRoles.Include(x => x.Role).Include(x => x.User).ToList();

        }

        public AspNetUser GetUserById(object id)
        {
            return context.AspNetUsers.Find(id);
        }
        public IEnumerable<AspNetRole> GetAllRoles()
        {
            return context.AspNetRoles.ToList();
        }
        public string GetRoleNameUsingRoleId(string roleId)
        {
            return context.AspNetRoles.Where(x => x.Id == roleId).FirstOrDefault().Name;
        }
        public string GetUserRoleId(string userId)
        {
            return context.AspNetUserRoles.Where(x => x.UserId == userId).FirstOrDefault().RoleId;
        }

        public IQueryable<SysUser> GetUsersIncludeRoles()
        {
            return context.SysUsers.AsNoTracking();

        }

        public void Delete(string id)
        {
            var user = GetUserById(id);
           // context.AspNetUserRoles.Find(id);
           //AspNetUser user= context.AspNetUsers.Find(id);
            context.Remove(user);
            context.SaveChanges();
        }
    }
}
