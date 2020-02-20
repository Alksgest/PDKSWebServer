using System.Collections.Generic;
using PdksBuisness.Dtos;
using PdksBuisness.Mappers;
using PdksPersistence.Models;
using PdksPersistence.Repositories;

namespace PdksBuisness.Managers
{
    public class ArticlesManager : IArticlesManager
    {
        private readonly IArticleRepository _articleRepository;

        private readonly ModelMapper _mapper;

        public ArticlesManager()
        {
            _articleRepository = new ArticleRepository();

            _mapper = ModelMapper.GetMapper;
        }

        public int AddArticle(ArticleDto article, UserRole accessLevel)
        {
            Article art = _mapper.Map<ArticleDto, Article>(article);
            art.AccessLevel = accessLevel;

            return _articleRepository.AddArticle(art);
        }

        public ArticleDto GetArticle(int id, UserRole role)
        {
            return _mapper.Map<Article, ArticleDto>(_articleRepository.GetArticle(id, role));
        }

        public IEnumerable<ArticleDto> GetArticles(int? categoryId, int? limit, UserRole role)
        {
            return _mapper.MapList<Article, ArticleDto>(_articleRepository.GetArticles(categoryId, limit, role));
        }

        public void UpdateArticle(ArticleDto article)
        {
            //_repository.UpdateArticle(article);
        }
    }
}
