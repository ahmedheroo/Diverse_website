﻿using Diverse_website.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Collections.Generic;
using System.Linq;

namespace Diverse_website.Repository
{
    public interface IUserRepo:IRepository<sys_user> 
    {
        IQueryable<sys_user> GetUsersIncludeRoles();
    }
}