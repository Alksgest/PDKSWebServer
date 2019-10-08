using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}