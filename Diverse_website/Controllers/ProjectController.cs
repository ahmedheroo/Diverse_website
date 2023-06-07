using Diverse_website.Models;
using Diverse_website.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.CSharp.RuntimeBinder;
using System.Threading;

namespace Diverse_website.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepo projectRepo;
        public ProjectController(IProjectRepo _projectRepo)
        {
            projectRepo = _projectRepo;
        }
        public IActionResult Index()
        {
            IQueryable<Project> model = projectRepo.GetAll();
            return View(model);
        }
    }
}
