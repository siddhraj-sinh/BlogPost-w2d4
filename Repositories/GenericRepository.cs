using BlogPost.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Repositories
{
    internal class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected readonly BloggingContext _context;

        public GenericRepository(BloggingContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
           _context.Set<T>().Update(entity);
        }
    }
}
