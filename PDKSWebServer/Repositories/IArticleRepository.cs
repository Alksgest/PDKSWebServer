using PDKSWebServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Repositories
{
    interface IArticleRepository
    {
        IEnumerable<Article> GetArticles();
        IEnumerable<Article> GetArticles(int categoryId);
        Article GetArticle(int id);
        int AddArticle(Article article);
        void UpdateArticle(Article article);
    }
}
