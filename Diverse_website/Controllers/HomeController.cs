using Diverse_website.Models;
using Diverse_website.Repository;
using Diverse_website.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Diverse_website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;

        private readonly IBlogsRepo blogsRepo;
        private readonly IProjectRepo projectsRepo;
        private readonly IVendorsRepo vendorsRepo;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment _webHostEnvironment, IBlogsRepo _blogsRepo,IProjectRepo _projectRepo,IVendorsRepo _vendorsRepo)
        {
            _logger = logger;
            webHostEnvironment = _webHostEnvironment;
            blogsRepo = _blogsRepo;
            projectsRepo = _projectRepo;
            vendorsRepo = _vendorsRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
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
            var item = blogsRepo.Getblogs().OrderByDescending(s => s.Id);
            var model = await PagingList.CreateAsync(item,100, page);   
            return View(model);
        }
        public async Task<IActionResult> Projects(int page = 1)
        {
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
            model.LatestProjects = projectsRepo.GetAll().OrderByDescending(s=>s.CreatedDate).Take(3);
            return View(model);
        }
        public async Task<IActionResult> Vendors(int page = 1)
        {
            var item = vendorsRepo.GetAll().OrderBy(s => s.Id);
            var model = await PagingList.CreateAsync(item, 100, page);
            return View(model);
        }
        
        public IActionResult Services()
        {
            return View();
        }
        public IActionResult Service1()
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
        public IActionResult AboutUs()
        {

            return View();
        }
        public IActionResult OurClients()
        {

            return View();
        }
        public IActionResult faq()
        {

            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
} 
    