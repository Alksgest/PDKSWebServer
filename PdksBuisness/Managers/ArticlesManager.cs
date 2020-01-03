using System.Collections.Generic;
using PdksBuisness.Dtos;
using PdksBuisness.Mappers;
using PdksPersistence.Models;
using PdksPersistence.Repositories;

namespace PdksBuisness.Managers
{
    public class ArticlesManager : IArticlesManager
    {
        private readonly IArticleRepository _repository;
        private readonly ModelMapper _mapper;

        public ArticlesManager()
        {
            _repository = new ArticleRepository();
            _mapper = ModelMapper.GetMapper;
        }

        public int AddArticle(ArticleDto article)
        {
            Article art = _mapper.Map<ArticleDto, Article>(article);
            return _repository.AddArticle(art);
        }

        public ArticleDto GetArticle(int id)
        {
            return _mapper.Map<Article, ArticleDto>(_repository.GetArticle(id));
        }

        public IEnumerable<ArticleDto> GetArticles(int? categoryId, int? limit)
        {
            return _mapper.MapList<Article, ArticleDto>(_repository.GetArticles(categoryId, limit));
        }

        public void UpdateArticle(ArticleDto article)
        {
            //_repository.UpdateArticle(article);
        }
    }
}
