using Diverse_website.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Repository
{
    public class SearchRepo : Repository<KeyWordSearch>, ISearchRepo
    {
        private readonly Diverse_websiteContext context;
        public SearchRepo(Diverse_websiteContext _context)
        {
            context = _context;
        }
        public IQueryable<KeyWordSearch> FindPagesContainsKeyword(string querystring)
        {
            return context.KeyWordSearches.Where(k => k.PageContent.Contains(querystring)).AsNoTracking();
        }
    }
}
