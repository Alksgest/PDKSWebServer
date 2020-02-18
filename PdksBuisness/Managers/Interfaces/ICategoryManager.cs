using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PdksBuisness.Dtos;

namespace PdksBuisness.Managers
{
    public interface ICategoryManager
    {
        IEnumerable<CategoryDto> GetCategories();
        CategoryDto GetCategory(Int32 id);
        ActionResult<Int32> AddCategory(CategoryDto category);
    }
}
