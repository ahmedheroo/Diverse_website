using System.Collections.Generic;
using System.Linq;
using Diverse_website.Models;
using Diverse_website.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Diverse_website.Repository
{
    public class BlogsRepo : Repository<Blog>, IBlogsRepo
    {
        private readonly Diverse_websiteContext context;
        public BlogsRepo(Diverse_websiteContext _context)
        {
            context = _context;
        }
        public IQueryable<Blog> Getblogs()
        {
            return context.Blogs.AsNoTracking();
        }

        public Blog GetBlogWithImage()
        {
            return context.Blogs.FirstOrDefault();
        }
        public IQueryable<Blog> GetAllblogsWithLatest()
        {
            return context.Blogs.AsNoTracking().Reverse().Take(3);
        }

        public IEnumerable<Counrty> GetAllCountries()
        {
            return context.Counrties.ToList();
        }

        public string GetCountryNameUsingCountryId(int Id)
        {
            return context.Counrties.Where(x => x.Id == Id).FirstOrDefault().CountryName;
        }
    }
}
