using Diverse_website.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Diverse_website.Repository
{
    public interface IUserRepo 
    {
        IQueryable<SysUser> GetUsersIncludeRoles();
        IEnumerable<AspNetUserRole> GetAllAdmins();
        IEnumerable<AspNetUserRole> GetAllUsers();
        AspNetUser GetUserById(object id);
        IEnumerable<AspNetRole> GetAllRoles();
        string GetRoleNameUsingRoleId(string roleId);
        string GetUserRoleId(string userId);
        void Delete(string id);
    }
}
