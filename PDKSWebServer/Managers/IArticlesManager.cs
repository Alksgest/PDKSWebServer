using PDKSWebServer.Dtos;
using PDKSWebServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Managers
{
    interface IArticlesManager
    {
        public int AddArticle(ArticleDto article);

        public ArticleDto GetArticle(int id, User.UserRole role = User.UserRole.NotAuthorized);

        public IEnumerable<ArticleDto> GetArticles(int? categoryId, int? limit, User.UserRole role = User.UserRole.NotAuthorized);

        public void UpdateArticle(ArticleDto article);
    }
}
