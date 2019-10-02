using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PDKSWebServer.Dtos;
using PDKSWebServer.Exceptions;
using PDKSWebServer.Managers;
using PDKSWebServer.Messages;
using PDKSWebServer.Models;
using PDKSWebServer.Repositories;

namespace PDKSWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("AllowAll")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticlesManager _articlesManager;
        private readonly IAuthorizationManager _authManager;

        public ArticlesController()
        {
            _articlesManager = new ArticlesManager();
            _authManager = new AuthorizationManager();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ArticleDto>> GetArticles()
        {
            var h = HttpContext.Request.Path;

            var json = this.Request.Query["token"];
            if (!String.IsNullOrEmpty(json))
            {
                var token = JsonConvert.DeserializeObject<AuthToken>(json);
            }

            //var role = token == null ? Models.User.UserRole.NotAuthorized : token.User.Role;
            return Ok(_articlesManager.GetArticles());
        }

        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ArticleDto>> GetArticles(int categoryId, AuthToken token = null)
        {
            var role = token == null ? Models.User.UserRole.NotAuthorized : token.User.Role;
            return Ok(_articlesManager.GetArticles(categoryId, role));
        }

        [HttpGet("{id}")]
        public ArticleDto GetArticle(int id, AuthToken token = null)
        {
            var role = token == null ? Models.User.UserRole.NotAuthorized : token.User.Role;
            return _articlesManager.GetArticle(id, role);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<int> PostArticle([FromBody]JsonElement request)
        {
            var req = request.GetRawText();
            CreateArticleRequestMessage msg = JsonConvert.DeserializeObject<CreateArticleRequestMessage>(req);

            try
            {
                _authManager.AllowAction(msg.Token);                      

                return _articlesManager.AddArticle(msg.Article);
            }
            catch(DoesNotHavePermissionsException e)
            {
                return BadRequest(e.Message);
            }
            catch(AuthorizationIsNeededException e)
            {
                return Unauthorized(e.Message);
            }
        }

    }
}