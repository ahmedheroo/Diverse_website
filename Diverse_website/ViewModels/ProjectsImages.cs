using Diverse_website.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.ViewModels
{
    public class ProjectsImages
    {
        public Project projects { get; set; }
        public IQueryable<Project> LatestProjects { get; set; }
        public IFormFile ProjectImage { get; set; }
    }
}
