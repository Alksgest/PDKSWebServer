using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDKSWebServer.DbContexts;
using PDKSWebServer.Models;

namespace PDKSWebServer.Repositories
{
    public class CategoryRepository : ICategoryRepository, IDisposable
    {

        private bool disposed = false;

        private readonly MainContext _db = new MainContext();

        public void AddCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _db.Categories;
        }

        public Category GetCategory(int id)
        {
            return _db.Categories.SingleOrDefault(cat => cat.ID == id);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _db.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
