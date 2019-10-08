using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PDKSWebServer.Dtos;

namespace PDKSWebServer.Managers
{
    interface ICategoryManager
    {
        IEnumerable<CategoryDto> GetCategories();
        CategoryDto GetCategory(Int32 id);
        ActionResult<Int32> AddCategory(CategoryDto category);
    }
}
