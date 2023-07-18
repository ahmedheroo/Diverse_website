using Diverse_website.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Repository
{
    public class VendorsRepo : Repository<Vendor>, IVendorsRepo
    {
        private readonly Diverse_websiteContext context;
        public VendorsRepo(Diverse_websiteContext _context)
        {
            context = _context;
        }
        public IQueryable<Vendor> GetVendorsIncludeCountries()
        {
            return context.Vendors.AsNoTracking();

        }
    }
}
