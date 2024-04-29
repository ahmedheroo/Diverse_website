using Diverse_website.Data;
using Diverse_website.Models;
using Diverse_website.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Controllers
{
    [Authorize("AdminRole")]

    public class SMTPSettingsController : BaseController
    {
        private readonly ISMTPSettingsRepo smtpSettingsRepo;
        public SMTPSettingsController(ISMTPSettingsRepo _smtpSettingsRepo)
        {
            smtpSettingsRepo = _smtpSettingsRepo;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var item = smtpSettingsRepo.GetAll().OrderBy(s => s.Id);
            var model = await PagingList.CreateAsync(item, 100, page);

            return View(model);
            
        }
        [HttpGet]
        public IActionResult Create() 
        {
            SMTPSetting model = new SMTPSetting();
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(SMTPSetting model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model = new SMTPSetting() 
                    {
                        HostName=model.HostName,
                        EmailFrom=model.EmailFrom,
                        PassWordEmailFrom=model.PassWordEmailFrom,
                        EmailTo=model.EmailTo
                    
                    };
                    smtpSettingsRepo.Insert(model);
                    NotifyAlert("success", "SMTP Settings has been saved");
                    return RedirectToAction("Index", "SMTPSettings");
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
            SMTPSetting model = new SMTPSetting();
            model = smtpSettingsRepo.GetById(Id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(SMTPSetting model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model = new SMTPSetting()
                    {
                        Id=model.Id,
                        HostName = model.HostName,
                        EmailFrom = model.EmailFrom,
                        PassWordEmailFrom = model.PassWordEmailFrom,
                        EmailTo = model.EmailTo

                    };
                    smtpSettingsRepo.Update(model);
                    NotifyAlert("success", "SMTP Settings has been Updated");
                    return RedirectToAction("Index", "SMTPSettings");
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
            SMTPSetting model = smtpSettingsRepo.GetById(Id);
             
            try
            {
                NotifyAlert("success", "SMTP has been deleted");
                smtpSettingsRepo.Delete(Id);
                return RedirectToAction("Index");

            }
            catch
            {

                NotifyAlert("error", "An error has occured !!", NotificationType.error);
                return RedirectToAction("Index");

            }


        }
    }
}
