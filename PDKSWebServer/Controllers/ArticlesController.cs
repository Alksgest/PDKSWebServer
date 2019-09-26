using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PDKSWebServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> GetArticles()
        {
            return Enumerable.Range(1, 5).Select(index => $"sus {index}");
        }
    }
}