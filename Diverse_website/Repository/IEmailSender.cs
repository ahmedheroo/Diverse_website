using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Repository
{
   public interface IEmailSender
    {
          Task SendEmailasync(string email, string subject, string htmlcontent);
    }
}
