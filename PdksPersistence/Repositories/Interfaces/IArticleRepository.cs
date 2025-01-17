﻿using PdksPersistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PdksPersistence.Repositories
{
    public interface IArticleRepository
    {
        IEnumerable<Article> GetArticles(int? categoryId, int? limit, UserRole permission);
        Article GetArticle(int id, UserRole permission);
        int AddArticle(Article article);
        void UpdateArticle(Article article);
    }
}
