using System.Linq;

namespace Diverse_website.Repository
{
    public class BlogsRepo<T> : IBlogsRepo<T> where T : class
    {
        public void Delete(object id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public T GetById(object id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(T obj)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
