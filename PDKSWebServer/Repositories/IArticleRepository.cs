﻿using PDKSWebServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PDKSWebServer.Repositories
{
    interface IArticleRepository
    {
        IEnumerable<Article> GetArticles(User.UserRole role);
        IEnumerable<Article> GetArticles(int categoryId, User.UserRole role);
        Article GetArticle(int id, User.UserRole role);
        int AddArticle(Article article);
        void UpdateArticle(Article article);
    }
}
