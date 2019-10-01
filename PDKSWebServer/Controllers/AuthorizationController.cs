using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PDKSWebServer.Dtos;
using PDKSWebServer.Exceptions;
using PDKSWebServer.Managers;

namespace PDKSWebServer.Controllers
{
    [Route("api/")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationManager _manager = new AuthorizationManager();

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthToken> Login([FromBody]JsonElement credentials) //
        {
            var val = credentials.GetRawText();
            AccountCredenials cre = JsonConvert.DeserializeObject<AccountCredenials>(val);
            try
            {
                var token = _manager.Login(cre);
                return token;
            }
            catch (UserDoesNotExistException)
            {
                return new BadRequestResult();
            }
        }

        [HttpPost("logout")]
        public ActionResult Logout(AuthToken token)
        {
            //TODO: add some false check?
            _manager.Logout(token);

            return Ok();
        }
    }
}