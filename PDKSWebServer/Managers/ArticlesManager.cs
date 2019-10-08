using PDKSWebServer.Dtos;
using PDKSWebServer.Mappers;
using PDKSWebServer.Models;
using PDKSWebServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Managers
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

        public ArticleDto GetArticle(int id, User.UserRole role = User.UserRole.NotAuthorized)
        {
            return _mapper.Map<Article, ArticleDto>(_repository.GetArticle(id, role));
        }

        public IEnumerable<ArticleDto> GetArticles(int? categoryId, int? limit, User.UserRole role = User.UserRole.NotAuthorized)
        {
            return _mapper.MapList<Article, ArticleDto>(_repository.GetArticles(categoryId, limit, role));
        }

        public void UpdateArticle(ArticleDto article)
        {
            //_repository.UpdateArticle(article);
        }
    }
}
