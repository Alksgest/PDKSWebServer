using System.Text.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PdksBuisness.Dtos;
using PdksBuisness.Exceptions;
using PdksBuisness.Managers;

namespace PDKSWebServer.Controllers
{
    [Route("api/")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationManager _manager = new AuthorizationManager();

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> Login([FromBody]JsonElement credentials) 
        {
            var val = credentials.GetRawText();
            AccountCredenials cre = JsonConvert.DeserializeObject<AccountCredenials>(val);
            try
            {
                var token = _manager.Login(cre);
                return Ok(token);
            }
            catch (UserDoesNotExistException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Logout(string authToken)
        {
            //TODO: add some false check?
            string token = this.HttpContext.Request.Query["authToken"];
            _manager.Logout(token);

            return Ok();
        }
    }
}