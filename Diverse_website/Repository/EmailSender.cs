//using MailKit.Net.Smtp;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Http;
//using MimeKit;
//using MailKit.Net.Smtp;
using Diverse_website.Helpers;
using Diverse_website.Models;

namespace Diverse_website.Repository
{
    public class EmailSender : IEmailSender
    {
         Diverse_websiteContext context = new Diverse_websiteContext() ;

        public async Task SendEmailasync(string email, string subject, string htmlcontent)
        {

            var smtp = context.SMTPSettings.FirstOrDefault();

            var mail = smtp.EmailFrom; /*"website.send@diverseltd.com";*/
            var Pw = smtp.PassWordEmailFrom; /*"@urv!$!@N23o2";*/

            var client = new SmtpClient()
            {
                Host =smtp.HostName, /*"smtp.office365.com",*/
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mail, Pw),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 20000

            };
            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtp.EmailFrom),
                //From = new MailAddress("website.send@diverseltd.com"),
                Subject = subject,
                Body = htmlcontent,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            // Send the email
            await client.SendMailAsync(mailMessage);
          

        }

       
    }
}
