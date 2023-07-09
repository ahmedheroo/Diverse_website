using Diverse_website.Models;
using System.Linq;

namespace Diverse_website.Repository
{
    public interface IProjectRepo:IRepository<Project>
    {
        IQueryable<Project> GetProjects();

    }
}
