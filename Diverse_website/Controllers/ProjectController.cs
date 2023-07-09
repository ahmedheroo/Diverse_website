using Diverse_website.Models;
using Diverse_website.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.CSharp.RuntimeBinder;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using ReflectionIT.Mvc.Paging;
using Diverse_website.ViewModels;
using System;
using System.IO;
using Diverse_website.Data;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace Diverse_website.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProjectRepo projectRepo;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProjectController(IProjectRepo _projectRepo , IWebHostEnvironment _webHostEnvironment)
        {
            projectRepo = _projectRepo;
            webHostEnvironment = _webHostEnvironment;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            // IQueryable<Blog> model ;
            var item = projectRepo.GetProjects().OrderBy(s => s.Id);
            var model = await PagingList.CreateAsync(item, 100, page);

            return View(model);
        }
        public IActionResult ViewProject(int Id)
        {
            ProjectsImages model = new ProjectsImages();
            model.projects = projectRepo.GetById(Id);
          //  model.projects.DescEn = WebUtility.HtmlDecode(model.projects.DescEn);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(ProjectsImages model)
        {
            if (ModelState.IsValid)
            {
               
                try
                {
                    string uniqueFileName = UploadedFile(model);

                    Project project = new Project()
                    {
                        TitleEn = model.projects.TitleEn,
                        TitleAr = model.projects.TitleAr,
                        DescEn = model.projects.DescEn,
                        DescAr = WebUtility.HtmlEncode(model.projects.DescAr),
                        PhotoUrl = uniqueFileName,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now

                    };
                    //try save data into database
                    NotifyAlert("success", "Project has been saved ");
                    projectRepo.Insert(project);
                return RedirectToAction("Index", "Project");
                }
                catch (Exception)
                {
                    NotifyAlert("error", "An error has occured !!", NotificationType.error);
                    return View(model);

                }

            }
            NotifyAlert("error", "An error has occured !!", NotificationType.error);

            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProjectsImages model = new ProjectsImages()
            {
                projects = projectRepo.GetById(id)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ProjectsImages model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = UploadedFile(model);

                    if (model.ProjectImage != null)
                    {
                        model.projects.PhotoUrl = UploadedFile(model);

                    }
                    Project project = new Project()
                    {
                        Id = model.projects.Id,
                        TitleEn = model.projects.TitleEn,
                        TitleAr = model.projects.TitleAr,
                        DescEn = model.projects.DescEn,
                        DescAr = model.projects.DescAr,
                        PhotoUrl = uniqueFileName,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now

                    };

                    NotifyAlert("success", "Project has been updated");
                    projectRepo.Update(project);
                    return RedirectToAction("Index");
                }
                catch (Exception)
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
            Project model = projectRepo.GetById(Id);
            if (model.PhotoUrl != null)
            {
                string ExitingFile = Path.Combine(webHostEnvironment.WebRootPath, model.PhotoUrl);
                ExitingFile = ExitingFile.Replace("~/", "");
                System.IO.File.Delete(ExitingFile);
            }
            try
            {

                NotifyAlert("success", "Project has been deleted");
                projectRepo.Delete(Id);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                NotifyAlert("error", "An error has occured !!", NotificationType.error);
                return View(model);

            }

        }
        private string UploadedFile(ProjectsImages model)
        {
            string uniqueFileName = null;
            string ImgUrl = null;

            if (model.ProjectImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProjectImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                ImgUrl = "~/Uploads/" + uniqueFileName;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProjectImage.CopyTo(fileStream);
                }
            }
            return ImgUrl;
        }
    }
}
