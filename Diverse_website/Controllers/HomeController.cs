using Diverse_website.Helpers;
using Diverse_website.Models;
using Diverse_website.Repository;
using Diverse_website.ViewModels;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Web;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Identity;

namespace Diverse_website.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        private readonly IBlogsRepo blogsRepo;
        private readonly IProjectRepo projectsRepo;
        private readonly IVendorsRepo vendorsRepo;
        private readonly IEmailSender emailSender;
        private readonly ISearchRepo searchRepo;

        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;

        static ScrapingBrowser _scrapbrowser = new ScrapingBrowser();
        //Egypt 1 , Saudi Arabia 2, Kenya 3, Germany
        //IPa---- EG , KE , SA
        // public string UserCountryName = "";

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment _webHostEnvironment, IBlogsRepo _blogsRepo, IProjectRepo _projectRepo, IVendorsRepo _vendorsRepo,IEmailSender _emailSender, IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider, ISearchRepo _searchRepo)
        {
            _logger = logger;
            webHostEnvironment = _webHostEnvironment;
            blogsRepo = _blogsRepo;
            projectsRepo = _projectRepo;
            vendorsRepo = _vendorsRepo;
            emailSender = _emailSender;
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            searchRepo = _searchRepo;
        }

        public IActionResult Index()
        {
            //TempData["IP"] = GetCountry();
            // Search();
            //SearchVM model = new SearchVM();
            //model.LatestBlogs = blogsRepo.Getblogs().OrderByDescending(s => s.CreatedDate);
            //Search();
             
            return View();
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl="~/Home")
        {

            Response.Cookies.Append(
                 CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            return LocalRedirect(returnUrl);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Blogs(int page = 1)
        {

            //var item = (dynamic)null;
            //if ( GetCountry() == "EG")
            //{
            //    item = blogsRepo.Getblogs().Where(s => s.CountryId == 1).OrderByDescending(s => s.Id);

            //}
            //else if ( GetCountry() == "SA")
            //{
            //    item = blogsRepo.Getblogs().Where(s => s.CountryId == 2).OrderByDescending(s => s.Id);

            //}
            //else if ( GetCountry() == "KE")
            //{
            //    item = blogsRepo.Getblogs().Where(s => s.CountryId == 3).OrderByDescending(s => s.Id);

            //}
            //else  
            //{
            //    item = blogsRepo.Getblogs().OrderByDescending(s => s.Id);

            //}
            var item = blogsRepo.Getblogs().OrderByDescending(s => s.Id);
            var model = await PagingList.CreateAsync(item, 100, page);
            return View(model);
        }
        public async Task<IActionResult> Projects(int page = 1)
        {
            //var item = (dynamic)null;
            //if (GetCountry() == "EG")
            //{
            //    item = projectsRepo.GetProjects().Where(c => c.CountryId == 1).OrderBy(s => s.Id);

            //}
            //else if (GetCountry() == "SA")
            //{
            //    item = projectsRepo.GetProjects().Where(c => c.CountryId == 2).OrderBy(s => s.Id);

            //}
            //else if (GetCountry() == "KE")
            //{
            //    item = projectsRepo.GetProjects().Where(c => c.CountryId == 3).OrderBy(s => s.Id);

            //}
            //else 
            //{ 
            // item = projectsRepo.GetProjects().OrderBy(s => s.Id);

            //}
            var item = projectsRepo.GetProjects().OrderBy(s => s.Id);

            var model = await PagingList.CreateAsync(item, 100, page);
            return View(model);
        }
        public IActionResult ViewBlog(int Id)
        {
            BlogsWithImages model = new BlogsWithImages();
            model.Blog = blogsRepo.GetById(Id);
            model.LatestBlogs = blogsRepo.GetAll().OrderByDescending(s => s.CreatedDate).Take(3);

            return View(model);
        }
        public IActionResult ViewProject(int Id)
        {
            ProjectsImages model = new ProjectsImages();
            model.projects = projectsRepo.GetById(Id);
            // model.projects.DescAr = WebUtility.HtmlDecode(model.projects.DescAr);
            model.LatestProjects = projectsRepo.GetAll().OrderByDescending(s => s.CreatedDate).Take(3);
            return View(model);
        }
        public async Task<IActionResult> Vendors(int page = 1)
        {
            //var item = (dynamic)null;
            //if (GetCountry() == "EG")
            //{
            //    item = vendorsRepo.GetAll().Where(c => c.CountryId == 1).OrderBy(s => s.Id);

            //}
            //else if (GetCountry() == "SA")
            //{
            //    item = vendorsRepo.GetAll().Where(c => c.CountryId == 2).OrderBy(s => s.Id);

            //}
            //else if (GetCountry() == "KE")
            //{
            //    item = vendorsRepo.GetAll().Where(c => c.CountryId == 3).OrderBy(s => s.Id);

            //}
            //else
            //{
            //    item = vendorsRepo.GetAll().OrderBy(s => s.Id);

            //}
            var item = vendorsRepo.GetAll().OrderBy(s => s.Id);

            var model = await PagingList.CreateAsync(item, 100, page);
            return View(model);
        }

        public IActionResult Services()
        {
            return View();
        }
        public IActionResult Consultancy()
        {
            return View();
        }
        public IActionResult support()
        {
            return View();
        }
        public IActionResult Implementation()
        {
            return View();
        }
        public IActionResult Solutions()
        {
            return View();
        }
        public IActionResult Solution1()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(SendEmail model)
        {
               Diverse_websiteContext ctx=new Diverse_websiteContext();
            string mailto = ctx.SMTPSettings.Select(c => c.EmailTo).FirstOrDefault();
            try
            {
                string htmlContent = "<body style=\"background - color: #F5F5F5;\"><div class=\"card\" style=\"background-color: #ffff;\"><div class=\"card-header\"><img src = \"http://diverseltd.com/client/assets/images/diverse%20logo%20cut.png \" style=\"width: 50px;\"></div><div class=\"card-body\"><h4 class=\"card-title\" style=\"padding-top: 20px;\">Dears all Diverse Team, kindly be notified that a new contact has been registered with the below details:</h4><p class=\"card-text\"><h4>Contact Name: " + model.senderName + "</h3></p><p class=\"card-text\"><h4>Contact Email: " + model.senderemail + "</h3></p><p class=\"card-text\"><h4>Contact Phone: " + model.Phone + "</h3></p><p class=\"card-text\"><h4>Message: " + model.message + "</h3></p></div><div class=\"card-footer text-muted \" style=\"padding-top: 20px;font-size: 11px;color: #6C6C6C;\">Please do not reply to this email.Emails sent to this address will not be answered.Copyright © 2023 DiverseLtd Company. All rights reserved.</div></div></body> ";

                emailSender.SendEmailasync(mailto, "New Contact", htmlContent);

               // NotifyAlert("success", "Your Email has been sent successfully, Thanks for contacting us");
               // ViewData["EmailSent"] = true;
                return View();
            }
            catch (Exception)
            {

                //NotifyAlert("error", "There is an error happened in sending contact Email,Please Try again");
               // ViewData["EmailSent"] = false;
                return View();

            }
        }
        public IActionResult AboutUs()
        {

            return View();
        }
        public IActionResult OurClients()
        {
          

            return View();
        }
        [HttpPost]
        public IActionResult OurClients(SendEmail model)
        {
            Diverse_websiteContext ctx = new Diverse_websiteContext();
            string mailto = ctx.SMTPSettings.Select(c => c.EmailTo).FirstOrDefault();
            try
            {
                string htmlContent = "<body style=\"background - color: #F5F5F5;\"><div class=\"card\" style=\"background-color: #ffff;\"><div class=\"card-header\"><img src = \"http://diverseltd.com/client/assets/images/diverse%20logo%20cut.png \" style=\"width: 50px;\"></div><div class=\"card-body\"><h4 class=\"card-title\" style=\"padding-top: 20px;\">Dears all Diverse Team, kindly be notified that a new contact has been registered with the below details:</h4><p class=\"card-text\"><h4>Contact Name: " + model.senderName + "</h3></p><p class=\"card-text\"><h4>Contact Email: " + model.senderemail + "</h3></p><p class=\"card-text\"><h4>Contact Phone: " + model.Phone + "</h3></p><p class=\"card-text\"><h4>Message: " + model.message + "</h3></p></div><div class=\"card-footer text-muted \" style=\"padding-top: 20px;font-size: 11px;color: #6C6C6C;\">Please do not reply to this email.Emails sent to this address will not be answered.Copyright © 2023 DiverseLtd Company. All rights reserved.</div></div></body> ";

                emailSender.SendEmailasync(mailto, "New Contact", htmlContent);

                // NotifyAlert("success", "Your Email has been sent successfully, Thanks for contacting us");
                // ViewData["EmailSent"] = true;
                return View();
            }
            catch (Exception)
            {

                //NotifyAlert("error", "There is an error happened in sending contact Email,Please Try again");
                // ViewData["EmailSent"] = false;
                return View();

            }
        }
            public IActionResult faq()
        {

            return View();
        }
        public IActionResult Networking()
        {

            return View();
        }
        public IActionResult DataCenter()
        {

            return View();
        }
        public IActionResult CyberSecurity()
        {

            return View();
        }
        //public SearchVM GetPartialView()
        //{
        //       SearchVM model = new SearchVM();
        //       model.result= Search();

        //      return model;
        //}


        public static HtmlNode GetHtml(string url)
        {
            _scrapbrowser.IgnoreCookies = true;
            _scrapbrowser.Timeout = TimeSpan.FromMinutes(15);
            _scrapbrowser.Headers["user-agent"] = "Mozilla/4.0 (compatible ; Windows NT 5.1 ; MSIE 6.0)"
                + "compatible ; MSIE 6.0 ; Windows NT 5.1 ; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            WebPage _webpage = _scrapbrowser.NavigateToPage(new Uri(url));
            return _webpage.Html;
        }
        public static string Search()
        {
            //var IndexPage =      GetHtml("http://www.diverseltd.com").InnerText;
            //var Solutionspage =  GetHtml("http://www.diverseltd.com/Home/Solutions").InnerText;
            //var Solutionspage1 = GetHtml("http://www.diverseltd.com/Home/Networking").InnerText;
            //var Solutionspage2 = GetHtml("http://www.diverseltd.com/Home/DataCenter").InnerText;
            //var Solutionspage3 = GetHtml("http://www.diverseltd.com/Home/CyberSecurity").InnerText;
            //var servicespage =   GetHtml("http://www.diverseltd.com/Home/Services").InnerText;
            //var servicespage1 =  GetHtml("http://www.diverseltd.com/Home/Consultancy").InnerText;
            //var servicespage2 =  GetHtml("http://www.diverseltd.com/Home/Implementation").InnerText;
            //var servicespage3 =  GetHtml("http://www.diverseltd.com/Home/support").InnerText;
            //var Clientspage =    GetHtml("http://www.diverseltd.com/Home/OurClients").InnerText;
            //var ProjectsPage =   GetHtml("http://www.diverseltd.com/Home/Projects").InnerText;
            //var VendorsPage =    GetHtml("http://www.diverseltd.com/Home/Vendors").InnerText;
            //var BlogsPage =      GetHtml("http://www.diverseltd.com/Home/Blogs").InnerText;
            //string index = null;
            ////string[] foundpages = new string[] { };
            ////string[] pages = new string[] { IndexPage, Solutionspage, Solutionspage1, Solutionspage2, Solutionspage3, servicespage, servicespage1, servicespage2, servicespage3, Clientspage, ProjectsPage, BlogsPage, VendorsPage };
            //////foreach (var item in pages)
            //////{
            //if (IndexPage.Contains("Welcome to"))
            //{

            //    index = "found";
            //    return (index);

            //}
            //else
            //{
            //    return ("Not");
            //}


            return ("Not");
            //}


        }
      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //static public string GetCountry()
        //{
        //    try
        //    {
        //        IPInfo iPInfo = new IPInfo();
        //        string url = "https://ipinfo.io?token=f79a7192272d97";
        //        var info = new WebClient().DownloadString(url);
        //        iPInfo = JsonConvert.DeserializeObject<IPInfo>(info);
        //        string UserCountryName = iPInfo.Country;
        //        return UserCountryName;
        //    }
        //    catch 
        //    {

        //        return "WO";
        //    }


        //}


    }
}
