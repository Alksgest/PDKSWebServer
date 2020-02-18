﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public ArticlesController(IArticlesManager manager)
        {
            _articlesManager = manager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<IEnumerable<ArticleDto>> GetArticles()
        {
            int? category = null;
            int? limit = null;
            AuthToken authToken = null;

            try
            {
                _ = Int32.TryParse(this.HttpContext.Request.Query["category"].ToString(), out Int32 cat);
                category = cat;

                _ = Int32.TryParse(this.HttpContext.Request.Query["limit"].ToString(), out Int32 lim);
                limit = lim;

                string token = this.HttpContext.Request.Query["authToken"];
                authToken = JsonConvert.DeserializeObject<AuthToken>(token);
            }
            catch { }
            return Ok(_articlesManager.GetArticles(category, limit));
            //return Ok(_articlesManager.GetArticles(0, 0, null));
        }



        [HttpGet("{id}")]
        public ArticleDto GetArticle(int id)
        {
            string token = this.HttpContext.Request.Query["authToken"];
            var authToken = JsonConvert.DeserializeObject<AuthToken>(token);

            var role = authToken?.User.Role;
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

            //try
            //{
            //    _authManager.AllowAction(msg.Token);

            //    return _articlesManager.AddArticle(msg.Article);
            //}
            //catch (DoesNotHavePermissionsException e)
            //{
            //    return BadRequest(e.Message);
            //}
            //catch (AuthorizationIsNeededException e)
            //{
            //    return Unauthorized(e.Message);
            //}


            return _articlesManager.AddArticle(msg.Article);
        }

    }
}