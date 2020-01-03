using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PdksPersistence.DbContexts;
using PdksPersistence.Models;

namespace PdksPersistence.Repositories
{
    public class ArticleRepository : IArticleRepository, IDisposable
    {
        private bool disposed = false;

        private readonly MainContext _db = new MainContext();

        public int AddArticle(Article article)
        {
            article.Author = _db.Users
                .FirstOrDefault(u => u.ID == article.Author.ID);

            article.Category = _db.Categories
                .FirstOrDefault(cat => cat.Id == article.Category.Id);

            _db.Articles.Add(article);
            var res = _db.SaveChanges();
            return res;
        }

        public Article GetArticle(int id)
        {
            return _db.Articles
                .Include(article => article.Author)
                .Include(article => article.Category)
                .AsEnumerable()
                .Where(article => true)
                    //role == null ? article.AccessLevel == null :
                    //(int)article.AccessLevel.GetValueOrDefault() >= (int)role.GetValueOrDefault())
                .SingleOrDefault(article => article.ID == id);
        }

        public IEnumerable<Article> GetArticles(int? categoryId, int? limit)
        {
            bool isNoCategory = (categoryId == null || categoryId == 0);

            limit = limit == null || limit == 0 ? 10 : limit;

            if (isNoCategory)
                return _db.Articles
                        .Include(article => article.Author)
                        .Include(article => article.Category)
                        .AsEnumerable()
                        .Where(article => true)
                            //role == null ? article.AccessLevel == null :
                            //(int)article.AccessLevel.GetValueOrDefault() >= (int)role.GetValueOrDefault())
                        .Skip(0)
                        .Take(limit.Value);

            return _db.Articles
                .Include(article => article.Author)
                .Include(article => article.Category)
                .AsEnumerable()
                .Where(article =>
                    (isNoCategory ? true : article.Category.Id == categoryId) )
                    //(role == null ? article.AccessLevel == null :
                    //(int)article.AccessLevel.GetValueOrDefault() >= (int)role.GetValueOrDefault()))
                .Skip(0)
                .Take(limit.Value);
        }

        public void UpdateArticle(Article article)
        {

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
