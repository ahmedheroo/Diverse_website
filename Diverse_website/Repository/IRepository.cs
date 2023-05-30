using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
       // Task<PaginatedList<T>> GetAllWithPagination(int pageNumber, int pageSize);
        T GetById(object id);
        void Insert(T obj);
       // void AddRange(List<T> obj);
        void Update(T obj);
        void Delete(object id);
    }
}
