using Diverse_website.Data;
using Diverse_website.Models;
using Diverse_website.Repository;
using Diverse_website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Diverse_website.Controllers
{
   
    [Authorize]
    public class BlogsController : BaseController
    {
       
        private readonly IBlogsRepo blogsRepo;
        private readonly IWebHostEnvironment webHostEnvironment;
        public BlogsController(IBlogsRepo _blogsRepo,IWebHostEnvironment _webHostEnvironment)
        {
            blogsRepo = _blogsRepo;
            webHostEnvironment = _webHostEnvironment;
        }
        public async Task<IActionResult> Index(int page=1)
        {
             // IQueryable<Blog> model ;
              var item = blogsRepo.Getblogs().OrderByDescending(s=>s.CreatedDate);
              var model =await PagingList.CreateAsync(item, 100, page);
 
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
                
                
                try
                {
                    string uniqueFileName = UploadedFile(model);
                    //  string username = GetUserId(model);

                    Blog blog = new Blog()
                    {

                        TitleEn = model.Blog.TitleEn,
                        TitleAr = model.Blog.TitleAr,
                        ContentEn = model.Blog.ContentEn,
                        ContentAr = model.Blog.ContentAr,
                        PhotoUrl = uniqueFileName,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        Tag = model.Blog.Tag,
                        userid = model.Blog.userid


                    };
                    NotifyAlert("success", "Blog has been saved");
                    blogsRepo.Insert(blog);
                    return RedirectToAction("Index", "Blogs");
                }
                catch  
                {

                    NotifyAlert("error", "An error has occured !!", NotificationType.error);
                    return View(model);
                }
              
            }
            NotifyAlert("error", "An error has occured !!", NotificationType.error);
            return View(model);
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
                    
                try
                {
                    string uniqueFileName = UploadedFile(model);

                    if (model.BlogImage != null)
                    {
                        model.Blog.PhotoUrl = UploadedFile(model);

                    }
                    Blog blog = new Blog()
                    {
                        Id = model.Blog.Id,
                        TitleEn = model.Blog.TitleEn,
                        TitleAr = model.Blog.TitleAr,
                        ContentEn = model.Blog.ContentEn,
                        ContentAr = model.Blog.ContentAr,
                        PhotoUrl = uniqueFileName,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                        userid = model.Blog.userid


                    };
                    NotifyAlert("success", "Blog has been updated");
                    blogsRepo.Update(blog);
                    return RedirectToAction("Index");
                }
                catch  
                {

                    NotifyAlert("error", "An error has occured !!", NotificationType.error);
                    return View(model);
                }
                  

                }  
                    NotifyAlert("error", "An error has occured !!", NotificationType.error);
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
            try
            {
                NotifyAlert("success", "Blog has been deleted");
                blogsRepo.Delete(Id);
                return RedirectToAction("Index");

            }
            catch
            {

                NotifyAlert("error", "An error has occured !!", NotificationType.error);
                return RedirectToAction("Index");

            }
             

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
        //private string GetUserId(BlogsWithImages model)
        //{
        //    string userName = null;
        //    userName = model.user.UserName;
        //    return userName;
        //}
    }
}
