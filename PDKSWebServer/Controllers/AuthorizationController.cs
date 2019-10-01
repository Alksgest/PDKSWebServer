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
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationManager _manager = new AuthorizationManager();
        [HttpPost]
        public ActionResult<AuthToken> Login(AccountCredenials credenials)
        {
            var token = _manager.Login(credenials);

            return token;
        }
    }
}