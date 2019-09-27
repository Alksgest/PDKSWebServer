using PDKSWebServer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Managers
{
    interface IArticlesManager
    {
        public int AddArticle(ArticleDto article);

        public ArticleDto GetArticle(int id);

        public IEnumerable<ArticleDto> GetArticles();

        public IEnumerable<ArticleDto> GetArticles(int categoryId);

        public void UpdateArticle(ArticleDto article);
    }
}
