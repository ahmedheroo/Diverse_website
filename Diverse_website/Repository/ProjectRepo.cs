using Diverse_website.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Diverse_website.Repository
{
    public class ProjectRepo : Repository<Project>, IProjectRepo
    {
        private readonly Diverse_websiteContext context;
        public ProjectRepo(Diverse_websiteContext _context)
        {
            context = _context;
        }
        public IQueryable<Project> GetProjects()
        {
            return context.Projects.AsNoTracking();
        }
    }
}
