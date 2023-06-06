using System.Collections.Generic;
using System.Linq;
using Diverse_website.Models;
using Diverse_website.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Diverse_website.Repository
{
    public class BlogsRepo : Repository<blog>, IBlogsRepo
    {
        private readonly DiverseContext context;
        public BlogsRepo(DiverseContext _context)
        {
            context = _context;
        }
        public IQueryable<blog> Getblogs()
        {
            return context.blogs.AsNoTracking();
        }

        public blog GetBlogWithImage()
        {
            return context.blogs.FirstOrDefault();
        }
      

       
    }
}
