using OnlineLearningAppApi.Database;
using OnlineLearningAppApi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
                return repositories[typeof(T)] as IGenericRepository<T>;
            
            IGenericRepository<T> repo = new GenericRepository<T>(_context);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
