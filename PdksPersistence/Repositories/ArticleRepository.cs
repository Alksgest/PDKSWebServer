using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PdksPersistence.DbContexts;
using PdksPersistence.Models;

namespace PdksPersistence.Repositories
{
    public class ArticleRepository : BaseRepository<Article>, IArticleRepository
    {
        private readonly MainContext _db = new MainContext();

        public int AddArticle(Article article)
        {
            article.Author = _db.Users
                .FirstOrDefault(u => u.Id == article.Author.Id);

            article.Category = _db.Categories
                .FirstOrDefault(cat => cat.Id == article.Category.Id);

            _db.Articles.Add(article);
            return _db.SaveChanges();

            //return Add(article);
        }

        public Article GetArticle(int id, UserRole permission)
        {
            return GetAll()
                .Include(article => article.Author)
                .Include(article => article.Category)
                .AsEnumerable()
                .Where(article => (int)article.AccessLevel.Value >= (int)permission)
                .SingleOrDefault(article => article.Id == id);
        }

        public IEnumerable<Article> GetArticles(int? categoryId, int? limit, UserRole permission)
        {
            bool isNoCategory = (categoryId == null || categoryId == 0);

            limit = limit == null || limit == 0 ? 10 : limit;

            IEnumerable<Article> result = null;

            if (isNoCategory)
            {
                result = GetAll()
                        .Include(article => article.Author)
                        .Include(article => article.Category)
                        .AsEnumerable()
                        .Where(article => {
                            var articleAccessLevel = (int)article.AccessLevel.Value;
                            var userAccessLevel = (int)permission;
                            return articleAccessLevel <= userAccessLevel;
                        })
                        .Skip(0)
                        .Take(limit.Value);
            }
            else
            {
                result = GetAll()
                    .Include(article => article.Author)
                    .Include(article => article.Category)
                    .AsEnumerable()
                    .Where(article => {
                        var articleAccessLevel = (int)article.AccessLevel.Value;
                        var userAccessLevel = (int)permission;
                        return articleAccessLevel <= userAccessLevel;
                    })
                    .Where(article => {
                        var id = article.Category.Id;
                        return id == categoryId;
                    })
                    .Skip(0)
                    .Take(limit.Value);
            }

            return result;
        }

        public void UpdateArticle(Article article)
        {

        }
    }
}
