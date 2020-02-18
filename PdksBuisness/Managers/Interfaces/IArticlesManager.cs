using System.Collections.Generic;
using PdksBuisness.Dtos;
using PdksPersistence.Models;

namespace PdksBuisness.Managers
{
    public interface IArticlesManager
    {
        int AddArticle(ArticleDto article);

        ArticleDto GetArticle(int id);

        IEnumerable<ArticleDto> GetArticles(int? categoryId, int? limit);

        void UpdateArticle(ArticleDto article);
    }
}
