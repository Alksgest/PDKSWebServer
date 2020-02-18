using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using PdksBuisness.Dtos;
using PdksBuisness.Exceptions;
using PdksBuisness.Managers;

namespace PDKSWebServer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationManager _manager;
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(IAuthorizationManager manager, ILogger<AuthorizationController> logger)
        {
            _manager = manager;
            _logger = logger;
        }

        [HttpGet("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> Login()
        {
            var rowCredentials = this.HttpContext.Request.Headers["Account-Credentials"];
            AccountCredenials credentials = JsonConvert.DeserializeObject<AccountCredenials>(rowCredentials);
            try
            {
                var token = _manager.Login(credentials);
                _logger.LogDebug($"User {credentials.Username} log in.");
                return Ok(token);
            }
            catch (UserDoesNotExistException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Logout()
        {
            string token = this.HttpContext.Request.Headers["Authorization"];
            _manager.Logout(token);

            return Ok();
        }
    }
}