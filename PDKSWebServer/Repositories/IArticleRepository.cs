using PDKSWebServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Repositories
{
    interface IArticleRepository
    {
        IEnumerable<Article> GetArticles(int? categoryId, int? limit, UserRole? role);
        Article GetArticle(int id, UserRole? role);
        int AddArticle(Article article);
        void UpdateArticle(Article article);
    }
}
