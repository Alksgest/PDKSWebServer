using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PDKSWebServer.Dtos;
using PDKSWebServer.Managers;

namespace PDKSWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryManager _manager;

        public CategoriesController()
        {
            _manager = new CategoryManager();
        }

        [HttpGet]
        public IEnumerable<CategoryDto> GetCategories()
        {
            return _manager.GetCategories();
        }

        [HttpGet("{id}")]
        public CategoryDto GetCategory(int id)
        {
            return _manager.GetCategory(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<int> PostCategory([FromBody]JsonElement request)
        {
            var req = request.GetRawText();
            CategoryDto category = JsonConvert.DeserializeObject<CategoryDto>(req);

            return Created(new Uri(""), _manager.AddCategory(category));
        }
    }
}