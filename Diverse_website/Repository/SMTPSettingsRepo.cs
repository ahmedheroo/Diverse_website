using Diverse_website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Repository
{
    public class SMTPSettingsRepo : Repository<SMTPSetting>, ISMTPSettingsRepo
    {
        private readonly Diverse_websiteContext context;
        public SMTPSettingsRepo(Diverse_websiteContext _context)
        {
            context = _context;
        }

    }
}
