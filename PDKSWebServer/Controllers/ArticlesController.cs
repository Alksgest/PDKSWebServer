using System;
using System.Collections.Generic;
using System.Text.Json;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using PdksBuisness.Dtos;
using PdksBuisness.Managers;
using PDKSWebServer.Messages;

namespace PDKSWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesManager _articlesManager;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(IArticlesManager manager, ILogger<ArticlesController> logger)
        {
            _articlesManager = manager;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<ArticleDto>> GetArticles()
        {
            try
            {
                var categoryId = this.HttpContext.Request.Query["category"].ToString();
                var count = this.HttpContext.Request.Query["limit"].ToString();

                _ = Int32.TryParse(categoryId, out Int32 cat);
                _ = Int32.TryParse(count, out Int32 lim);

                return Ok(_articlesManager.GetArticles(cat, lim));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet("{id}")]
        public ArticleDto GetArticle(int id)
        {
            var res = _articlesManager.GetArticle(id);
            return res;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("createArticle")]
        public ActionResult<int> PostArticle([FromBody]JsonElement request)
        {
            var req = request.GetRawText();
            CreateArticleRequestMessage msg = JsonConvert.DeserializeObject<CreateArticleRequestMessage>(req);

            return _articlesManager.AddArticle(msg.Article);
        }

    }
}