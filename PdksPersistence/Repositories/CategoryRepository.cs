using System;
using System.Collections.Generic;

using PdksPersistence.DbContexts;
using PdksPersistence.Models;

namespace PdksPersistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {

        public Int32 AddCategory(Category category)
        {
            return Add(category);
        }

        public IEnumerable<Category> GetCategories()
        {
            return GetAll();
        }

        public Category GetCategory(Int32 id)
        {
            return GetOne(id);
        }
    }
}
