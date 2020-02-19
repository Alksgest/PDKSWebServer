using System.Collections.Generic;
using PdksBuisness.Dtos;
using PdksPersistence.Models;

namespace PdksBuisness.Managers
{
    public interface IArticlesManager
    {
        int AddArticle(ArticleDto article);

        ArticleDto GetArticle(int id, UserRole permission);

        IEnumerable<ArticleDto> GetArticles(int? categoryId, int? limit, UserRole permission);

        void UpdateArticle(ArticleDto article);
    }
}
