using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PDKSWebServer.Dtos;
using PDKSWebServer.Managers;
using PDKSWebServer.Models;
using PDKSWebServer.Repositories;

namespace PDKSWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("AllowAll")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesManager _manager;
        public ArticlesController()
        {
            _manager = new ArticlesManager();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ArticleDto>> GetArticles()
        {
            return Ok(_manager.GetArticles());
        }

        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ArticleDto>> GetArticles(int categoryId)
        {
            return Ok(_manager.GetArticles(categoryId));
        }

        [HttpGet("{id}")]
        public ArticleDto GetArticle(int id)
        {
            return _manager.GetArticle(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<int> PostArticle([FromBody]JsonElement article)
        {
            var val = article.GetRawText();
            ArticleDto art = JsonConvert.DeserializeObject<ArticleDto>(val);

            return _manager.AddArticle(art);
        }

    }
}