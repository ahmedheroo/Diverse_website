using Diverse_website.Data;
using Diverse_website.Models;
using Diverse_website.Repository;
using Diverse_website.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Controllers
{
    [Authorize]
    public class VendorController : BaseController
    {
        private readonly IVendorsRepo vendorsRepo;
        private readonly IWebHostEnvironment webHostEnvironment;

        public VendorController(IVendorsRepo _vendorsRepo, IWebHostEnvironment _webHostEnvironment)
        {
            vendorsRepo = _vendorsRepo;
            webHostEnvironment = _webHostEnvironment;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            // IQueryable<Blog> model ;
            var item = vendorsRepo.GetAll().OrderByDescending(s => s.CreatedDate);
            var model = await PagingList.CreateAsync(item, 100, page);

            return View(model);
        }
        public IActionResult ViewVendor(int Id)
        {
            VendorsWithImagesVM model = new VendorsWithImagesVM();
            model.vendor = vendorsRepo.GetById(Id);

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(VendorsWithImagesVM model)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    string uniqueFileName = UploadedFile(model);

                    Vendor vendor = new Vendor()
                    {

                        TitleEn = model.vendor.TitleEn,
                        TitleAr = model.vendor.TitleAr,
                        DescEn = model.vendor.DescEn,
                        DescAr = model.vendor.DescAr,
                        PhotoUrl = uniqueFileName,
                        VendorUrl = model.vendor.VendorUrl,
                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now

                    };
                    NotifyAlert("success", "Vendor has been saved");
                    vendorsRepo.Insert(vendor);
                    return RedirectToAction("Index", "Vendor");
                }
                catch
                {

                    NotifyAlert("error", "An error has occured !!",NotificationType.error);
                    return View(model);
                }

            }
            NotifyAlert("error", "An error has occured !!", NotificationType.error);

            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            VendorsWithImagesVM model = new VendorsWithImagesVM()
            {
                vendor = vendorsRepo.GetById(Id)
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(VendorsWithImagesVM model)
        {

            if (ModelState.IsValid)
            {
                
                try
                {
                    string uniqueFileName = UploadedFile(model);

                    if (model.VendorImage != null)
                    {
                        model.vendor.PhotoUrl = UploadedFile(model);

                    }
                    Vendor vendor = new Vendor()
                    {
                        Id = model.vendor.Id,
                        TitleEn = model.vendor.TitleEn,
                        TitleAr = model.vendor.TitleAr,
                        DescEn = model.vendor.DescEn,
                        DescAr = model.vendor.DescAr,
                        VendorUrl = model.vendor.VendorUrl,
                        PhotoUrl = uniqueFileName,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now

                    };
                    NotifyAlert("success", "Vendor has been updated");
                    vendorsRepo.Update(vendor);
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
            Vendor model = vendorsRepo.GetById(Id);
            if (model.PhotoUrl != null)
            {
                string ExitingFile = Path.Combine(webHostEnvironment.WebRootPath, model.PhotoUrl);
                ExitingFile = ExitingFile.Replace("~/", "");
                System.IO.File.Delete(ExitingFile);
            }
            try
            {
                NotifyAlert("success", "Vendor has been Deleted");
                vendorsRepo.Delete(Id);
                return RedirectToAction("Index");

            }
            catch
            {

                NotifyAlert("error", "An error has occured !!", NotificationType.error);
                return RedirectToAction("Index");

            }


        }
        private string UploadedFile(VendorsWithImagesVM model)
        {
            string uniqueFileName = null;
            string ImgUrl = null;

            if (model.VendorImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.VendorImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                ImgUrl = "~/Uploads/" + uniqueFileName;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.VendorImage.CopyTo(fileStream);
                }
            }
            return ImgUrl;
        }
    }
}
