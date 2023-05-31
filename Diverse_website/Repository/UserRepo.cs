using Diverse_website.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Diverse_website.Repository
{
    public class UserRepo:Repository<sys_user>, IUserRepo
    {
        private readonly DiverseContext context;

        public UserRepo(DiverseContext _context)
        {
            context = _context;
        }
        public IQueryable<sys_user> GetUsersIncludeRoles()
        {
            return context.sys_Users.AsNoTracking();

        }

        
    }
}
