using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PdksPersistence.DbContexts;
using PdksPersistence.Models;

namespace PdksPersistence.Repositories
{
    public class CategoryRepository : ICategoryRepository, IDisposable
    {

        private bool disposed = false;

        private readonly MainContext _db = new MainContext();

        public Int32 AddCategory(Category category)
        {
            _db.Categories.Add(category);
            return _db.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _db.Categories;
        }

        public Category GetCategory(Int32 id)
        {
            return _db.Categories.SingleOrDefault(cat => cat.Id == id);
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
