using PdksPersistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PdksPersistence.Repositories
{
    interface IArticleRepository
    {
        IEnumerable<Article> GetArticles(int? categoryId, int? limit, UserRole? role);
        Article GetArticle(int id, UserRole? role);
        int AddArticle(Article article);
        void UpdateArticle(Article article);
    }
}
