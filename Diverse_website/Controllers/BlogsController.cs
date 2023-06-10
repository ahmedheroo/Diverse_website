using Diverse_website.Models;
using Diverse_website.Repository;
using Diverse_website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Diverse_website.Controllers
{ 
    [Authorize]
    public class BlogsController : Controller
    {
       
        private readonly IBlogsRepo blogsRepo;
        private readonly IWebHostEnvironment webHostEnvironment;
        public BlogsController(IBlogsRepo _blogsRepo,IWebHostEnvironment _webHostEnvironment)
        {
            blogsRepo = _blogsRepo;
            webHostEnvironment = _webHostEnvironment;
        }
        public IActionResult Index()
        {
              IQueryable<Blog> model ;
              model = blogsRepo.Getblogs();
 
            return View(model);
        }
        public IActionResult ViewBlog(int Id)
        {
            BlogsWithImages model = new BlogsWithImages();
            model.Blog = blogsRepo.GetById(Id);

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Create(BlogsWithImages model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                
                Blog blog= new Blog()
                {
                    TitleEn = model.Blog.TitleEn,
                    TitleAr = model.Blog.TitleAr,
                    ContentEn = model.Blog.ContentEn,
                    ContentAr = model.Blog.ContentAr,
                    PhotoUrl = uniqueFileName,
                    IsDeleted = false,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now

                };

                blogsRepo.Insert(blog);  
                return RedirectToAction("Index", "Blogs");
            }

            return View();
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            BlogsWithImages model = new BlogsWithImages()
            {
                Blog = blogsRepo.GetById(Id)
              };

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(BlogsWithImages model)
        {
            
                if (ModelState.IsValid)
                {
                    string uniqueFileName = UploadedFile(model);

                    if (model.BlogImage != null)
                    {
                        model.Blog.PhotoUrl = UploadedFile(model);

                    }
                    Blog blog = new Blog()
                    {
                        TitleEn = model.Blog.TitleEn,
                        TitleAr = model.Blog.TitleAr,
                        ContentEn = model.Blog.ContentEn,
                        ContentAr = model.Blog.ContentAr,
                        PhotoUrl = uniqueFileName,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now

                    };
                    blogsRepo.Update(model.Blog);
                    return RedirectToAction("Index");

                }       
                return View(model);
             
            
        }
        public IActionResult Delete(int Id)
        {
            Blog model = blogsRepo.GetById(Id);
            if (model.PhotoUrl != null)
            {
                string ExitingFile = Path.Combine(webHostEnvironment.WebRootPath, model.PhotoUrl);
                ExitingFile = ExitingFile.Replace("~/", "");
                System.IO.File.Delete(ExitingFile);
            }
 
            blogsRepo.Delete(Id);
            return RedirectToAction("Index"); 
        }
        private string UploadedFile(BlogsWithImages model)
        {
            string uniqueFileName = null;
            string ImgUrl = null;

            if (model.BlogImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.BlogImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                ImgUrl = "~/Uploads/" + uniqueFileName;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.BlogImage.CopyTo(fileStream);
                }
            }
            return ImgUrl;
        }
    }
}
