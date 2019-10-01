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

        public Article GetArticle(int id)
        {
            return _db.Articles
                .Include(article => article.Author)
                .Include(article => article.Category)
                .SingleOrDefault(a => a.ID == id);
            //return _db.Articles.Include(article => article.Author)
            //    .Include(article => article.ArticleCategories)
            //    .ThenInclude(ac => ac.Category)
            //    .SingleOrDefault(a => a.ArticleID == id);
            //return this.Articles.SingleOrDefault(a => a.ArticleID == id);
        }

        public IEnumerable<Article> GetArticles()
        {
            //var articles = _db.Articles
            //    .Include(article => article.Author)
            //    .Include(article => article.ArticleCategories)
            //    .ThenInclude(ac => ac.Category);
            //return articles;
            return _db.Articles
                .Include(article => article.Author)
                .Include(article => article.Category);
        }

        public IEnumerable<Article> GetArticles(int categoryId)
        {
            //var result = new List<Article>();
            //foreach(var art in this.Articles)
            //{
            //    foreach(var cat in art.Categories)
            //    {
            //        if(cat.CategoryId == categoryId)
            //        {
            //            result.Add(art);
            //            break;
            //        }
            //    }
            //}
            return _db.Articles
                .Include(article => article.Author)
                .Include(article => article.Category)
                .Where(article => article.Category.ID == categoryId); 
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
