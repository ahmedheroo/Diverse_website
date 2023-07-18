using Diverse_website.Models;
using Diverse_website.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Diverse_website.Repository
{
    public interface IBlogsRepo :IRepository<Blog>  
    {
        IQueryable<Blog> Getblogs();
        IQueryable<Blog> GetAllblogsWithLatest();
        Blog GetBlogWithImage();
        IEnumerable<Counrty> GetAllCountries();
        string GetCountryNameUsingCountryId(int Id);
        //void Insert(blog obj);

    }
}
