using Microsoft.AspNetCore.Mvc;
using Diverse_website.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
 

namespace Diverse_website.Controllers
{
    public class BaseController : Controller
    {

        public void NotifyAlert(string message, string title,
                                    NotificationType notificationType = NotificationType.success)
        {
            var msg = new
            {
                message = message,
                title = title,
                icon = notificationType.ToString(),
                type = notificationType.ToString(),
                provider = GetProviderAlert()
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }

        public void NotifyToastr(string message, string title,
                                    NotificationType notificationType = NotificationType.success)
        {
            var msg = new
            {
                message = message,
                title = title,
                icon = notificationType.ToString(),
                type = notificationType.ToString(),
                provider = GetProviderToastr()
            };

            TempData["Message"] = JsonConvert.SerializeObject(msg);
        }
        private string GetProviderAlert()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var value = configuration["NotificationProviderAlert"];

            return value;
        }
        private string GetProviderToastr()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            var value = configuration["NotificationProviderToastr"];

            return value;
        }
    }
}
