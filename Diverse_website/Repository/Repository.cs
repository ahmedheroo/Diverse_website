using Diverse_website.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Diverse_website.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DiverseContext Context;
        public DbSet<T> table = null;
        public Repository()
        {
            this.Context=new DiverseContext();
            table=Context.Set<T>();
        }
        public Repository(DiverseContext _context)
        {
            this.Context = _context;
            table = Context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return table.AsNoTracking();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T obj)
        {
            table.Add(obj);
            Context.SaveChanges();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            Context.Entry(obj).State = EntityState.Modified;
            Context.SaveChanges();
            Context.Entry(obj).State = EntityState.Detached;

        }
        public void Delete(object id)
        {
            T exist = table.Find(id);
            table.Remove(exist);
            Context.SaveChanges();
        }
    }
}
