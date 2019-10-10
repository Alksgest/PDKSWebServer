using System.Collections.Generic;
using PDKSWebServer.Dtos;
using PDKSWebServer.Models;

namespace PDKSWebServer.Managers
{
    interface IArticlesManager
    {
        public int AddArticle(ArticleDto article);

        public ArticleDto GetArticle(int id, UserRole? role);

        public IEnumerable<ArticleDto> GetArticles(int? categoryId, int? limit, UserRole? role);

        public void UpdateArticle(ArticleDto article);
    }
}
