using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PDKSWebServer.Dtos;
using PDKSWebServer.Mappers;
using PDKSWebServer.Models;
using PDKSWebServer.Repositories;

namespace PDKSWebServer.Managers
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository _repository;

        public CategoryManager()
        {
            _repository = new CategoryRepository();
        }

        public ActionResult<Int32> AddCategory(CategoryDto category)
        {
            var cat = ModelMapper.GetMapper.Map<CategoryDto, Category>(category);
            return _repository.AddCategory(cat);
        }

        public IEnumerable<CategoryDto> GetCategories()
        {
            return ModelMapper.GetMapper.MapList<Category, CategoryDto>(_repository.GetCategories());
        }

        public CategoryDto GetCategory(int id)
        {
            return ModelMapper.GetMapper.Map<Category, CategoryDto>(_repository.GetCategory(id));
        }
    }
}
