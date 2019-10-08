using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PDKSWebServer.DbContexts;
using PDKSWebServer.Models;

namespace PDKSWebServer.Repositories
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
                .FirstOrDefault(cat => cat.ID == article.Category.ID);

            //if (article.Author == null || article.Category == null)
            //    throw new ArgumentNullException("Author or category do not exist");

            _db.Articles.Add(article);
            var res = _db.SaveChanges();
            return res;
        }

        public Article GetArticle(int id, User.UserRole role)
        {
            return _db.Articles
                .Where(a => (int)a.AccessLevel >= (int)role)
                .Include(article => article.Author)
                .Include(article => article.Category)
                .SingleOrDefault(a => a.ID == id);
        }

        public IEnumerable<Article> GetArticles(int? categoryId, int? limit,  User.UserRole? role)
        {
            return _db.Articles
                .Include(article => article.Author)
                .Include(article => article.Category)
                .Where(article => article.Category.ID == categoryId || categoryId == 0)
                .Skip(0)
                .Take(limit.GetValueOrDefault() == 0 ? 10 : limit.GetValueOrDefault());
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
