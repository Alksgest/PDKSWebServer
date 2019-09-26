using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDKSWebServer.Models;
using PDKSWebServer.Repositories;

namespace PDKSWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleRepository _repo;
        public ArticlesController()
        {
            _repo = new ArticleRepository();
        }

        [HttpGet]
        public IEnumerable<Article> GetArticles()
        {
            return _repo.GetArticles();
        }

        [HttpGet("category/{categoryId}")]
        public IEnumerable<Article> GetArticles(int categoryId)
        {
            var res = _repo.GetArticles(categoryId);
            return res;
        }

        [HttpGet("{id}")]
        public Article GetArticle(int id)
        {
            return _repo.GetArticle(id);
        }

        [HttpPost]
        public int PostArticle(Article article)
        {
            return _repo.AddArticle(article);
        }

    }
}