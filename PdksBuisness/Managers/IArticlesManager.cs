using System.Collections.Generic;
using PdksBuisness.Dtos;
using PdksPersistence.Models;

namespace PdksBuisness.Managers
{
    interface IArticlesManager
    {
        int AddArticle(ArticleDto article);

        ArticleDto GetArticle(int id, UserRole? role);

        IEnumerable<ArticleDto> GetArticles(int? categoryId, int? limit, UserRole? role);

        void UpdateArticle(ArticleDto article);
    }
}
