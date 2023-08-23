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

namespace Diverse_website.Repository
{
    public class EmailSender : IEmailSender
    {

        public async Task SendEmailasync(string email, string subject, string htmlcontent)
        {
            //         var configuration = new ConfigurationBuilder()
            //.SetBasePath(Directory.GetCurrentDirectory())
            //.AddJsonFile("appsettings.json")
            //.Build();

            //         var smtpConfig = configuration.GetSection("SmtpConfig").Get<>();

            var mail = "website.send@diverseltd.com";
            var Pw = "@urv!$!@N23o2";

            var client = new SmtpClient()
            {
                Host = "smtp.office365.com",
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
                From = new MailAddress("website.send@diverseltd.com"),
                Subject = subject,
                Body = htmlcontent,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

            // Send the email
            await client.SendMailAsync(mailMessage);
            //var bodyBuilder = new BodyBuilder();
            //var template = System.IO.File.ReadAllText(templatePath);
            //foreach (var prop in model.GetType().GetProperties())
            //{
            //    template = template.Replace("{{" + prop.Name + "}}", prop.GetValue(model)?.ToString());
            //}

            //bodyBuilder.HtmlBody = template;
            //message.Body = bodyBuilder.ToMessageBody();

            //return client.SendMailAsync(

            //    new MailMessage(from: mail,
            //                    to: email,
            //                    subject,
            //                    message));

        }

        //----------------------------------

        //public void SendEmailasync(string email, string subject, string htmlcontent, object model)
        //{
        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("website.send@diverseltd.com", "website.send@diverseltd.com"));
        //    message.To.Add(new MailboxAddress(email,email));
        //    message.Subject = subject;
        //    message.Body = htmlcontent.tomessage;

        //    //var bodyBuilder = new BodyBuilder();
        //    //var template = File.ReadAllText(templatePath);

        //    // Replace placeholders in the template with actual values
        //    //foreach (var prop in model.GetType().GetProperties())
        //    //{
        //    //    template = template.Replace("{{" + prop.Name + "}}", prop.GetValue(model)?.ToString());

        //    //}


        //    //bodyBuilder.HtmlBody = template;
        //    //string
        //    //message.Body = bodyBuilder.ToMessageBody();


        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect("smtp.office365.com", 587, false);
        //        client.Authenticate("website.send@diverseltd.com", "@urv!$!@N23o2");
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }

        //}
    }
}
