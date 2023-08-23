using Diverse_website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Repository
{
    public interface ISearchRepo:IRepository<KeyWordSearch>
    {
        IQueryable<KeyWordSearch> FindPagesContainsKeyword(string querystring);
    }
}
