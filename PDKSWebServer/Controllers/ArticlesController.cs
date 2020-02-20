using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PdksBuisness.Dtos;
using PdksBuisness.Managers;
using PdksPersistence.Models;

namespace PDKSWebServer.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/articles")]
    [Produces("application/json")]
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
        public ActionResult<IEnumerable<ArticleDto>> GetArticles()
        {
            try
            {
                GetUserPermissions(out UserRole permissions);

                var categoryId = this.HttpContext.Request.Query["category"].ToString();
                var count = this.HttpContext.Request.Query["limit"].ToString();

                _ = Int32.TryParse(categoryId, out Int32 cat);
                _ = Int32.TryParse(count, out Int32 lim);

                var res = _articlesManager.GetArticles(cat, lim, permissions).ToList();

                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<ArticleDto> GetArticle(int id)
        {
            GetUserPermissions(out UserRole permissions);

            var res = _articlesManager.GetArticle(id, permissions);
            return Ok(res);
        }

        // POST: api/articles
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<int> PostArticle(ArticleDto article)
        {
            var authorized = Boolean.Parse(this.HttpContext.Request.Headers["Authorized"].ToString());

            if (!authorized) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(_articlesManager.AddArticle(article));
        }

        private void GetUserPermissions(out UserRole permissions)
        {
            var authorized = Boolean.Parse(this.HttpContext.Request.Headers["Authorized"].ToString());

            if (authorized)
            {
                var str = this.HttpContext.Request.Headers["Permissions"].ToString();
                _ = Enum.TryParse(str, out UserRole permission);
                permissions = permission;
            }
            else
            {
                permissions = UserRole.NotAuthorized;
            }
        }
    }
}